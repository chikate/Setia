using Microsoft.EntityFrameworkCore;
using Setia.Models.Structs;
using Setia.Services.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Setia.Controllers
{
    public class CRUDService<TModel, TContext> : ICRUD<TModel>
        where TModel : BaseModel
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TModel> _dbData;
        private readonly ILogger<CRUDService<TModel, TContext>> _logger;
        private readonly IAudit _audit;
        private readonly IAuth _auth;

        public CRUDService
        (
            TContext context,
            ILogger<CRUDService<TModel, TContext>> logger,
            IAudit audit,
            IAuth auth
        )
        {
            _context = context;
            _dbData = context.Set<TModel>();
            _logger = logger;
            _audit = audit;
            _auth = auth;
        }

        public async Task<IEnumerable<TModel>> Get(TModel? filter = default)
        {
            try
            {
                //_context.Set<UserModel>()
                //    .Where(e => e.Tags.Any(u => u.Contains("Dragos") || u.Contains("Admin")));

                IQueryable<TModel> query = _dbData
                    .Where(e => !e.Tags.Any(t => t.Contains("Deleted")))
                    .AsNoTracking();

                //foreach (string tag in await _auth.GetUserTags(""))
                //{
                //    if (!(await _auth.CheckUserRights(["Dragos"])).Any(c => c == "Dragos"))
                //    {
                //        query = query.Where(e => e.Author == _auth.GetCurrentUser().Username);
                //    }
                //}

                #region tests
                //switch (typeof(TModel).Name)
                //{
                //    //case nameof(TagModel):
                //    //    query = query.Where(t => ((TagModel)(object)t).Tag.MatchesLQuery(specific));
                //    //    break;
                //    //case nameof(UserTagModel):
                //    //    query = query.Where(ut => ((UserTagModel)(object)ut).Tag.MatchesLQuery(specific));
                //    //    if (user != null) query = query.Where(ut => ((UserTagModel)(object)ut).User.Contains(user));
                //    //    break;
                //    //case nameof(UserModel):
                //    //    if (user != null) query = query.Where(u => ((UserModel)(object)u).Username.Contains(user));
                //    //    break;
                //    case nameof(QuestionAnswerModel):
                //        query.Include("QuestionData");
                //        break;
                //    default:
                //        break;
                //}
                #endregion

                foreach (PropertyInfo property in typeof(TModel).GetProperties()
                    .Where(p => p.GetCustomAttributes(typeof(ForeignKeyAttribute), false).Any()))
                    query = query.Include(
                        property.Name.EndsWith("Id")
                            ? property.Name.Substring(0, property.Name.Length - 2) + "Data"
                            : property.Name + "Data"
                    );

                //if (filter != null)
                //    query = query.Where(e => property.GetValue(e).Equals(property.GetValue(filter)));

                if (filter != null)
                    foreach (PropertyInfo property in typeof(TModel).GetProperties())
                    {
                        object? filterValue = property.GetValue(filter);
                        if (filterValue != null && !string.IsNullOrEmpty(filterValue.ToString()))
                            query = query.Where(e => property.GetValue(e) != null && property.GetValue(e).Equals(filterValue));
                    }

                return await query.ToListAsync();
            }
            catch (Exception ex) { _logger.LogError(ex, this.GetType().FullName); throw; }
        }
        public async Task<TModel> Add(TModel model)
        {
            try
            {
                model.GetType().GetProperty("AuthorId")?.SetValue(model, _auth.GetCurrentUser()?.Id);
                //PropertyInfo? passwordProperty = model.GetType().GetProperty("Password");
                //passwordProperty?.SetValue(model, _auth.CriptPassword((string)passwordProperty?.GetValue(model)));

                await _dbData.AddAsync(model);
                await _context.SaveChangesAsync();
                await _audit.LogAuditTrail<TModel>(model);
                return model;
            }
            catch (Exception ex) { _logger.LogError(ex, this.GetType().FullName); throw; }
        }
        public async Task<TModel> Update(TModel model)
        {
            try
            {
                TModel oldModel = model;
                _context.Update(model);
                await _context.SaveChangesAsync();
                await _audit.LogAuditTrail(model, oldModel);
                return model;
            }
            catch (Exception ex) { _logger.LogError(ex, this.GetType().FullName); throw; }
        }
        public async Task Delete(string id)
        {
            try
            {
                TModel? itemToDelete = await _dbData.FindAsync(id);
                if (itemToDelete != null)
                {
                    _dbData.Remove(itemToDelete);
                    await _context.SaveChangesAsync();
                    await _audit.LogAuditTrail<TModel>(itemToDelete);
                }
            }
            catch (Exception ex) { _logger.LogError(ex, this.GetType().FullName); throw; }
        }
    }
}