using Microsoft.EntityFrameworkCore;
using Setia.Models.Base;
using Setia.Models.Gov;
using Setia.Services.Interfaces;
using System.Reflection;

namespace Setia.Controllers
{
    public class CRUDService<TModel, TContext> : ICRUD<TModel> where TModel : class where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TModel> _contextTable;
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
            _contextTable = context.Set<TModel>();
            _logger = logger;
            _audit = audit;
            _auth = auth;
        }

        public async Task<IEnumerable<TModel>> Get(TModel? filter = null)
        {
            try
            {
                if (filter != null)
                {
                }
                IQueryable<TModel> query = _contextTable.AsNoTracking().AsQueryable();
                switch (typeof(TModel).Name)
                {
                    //case nameof(TagModel):
                    //    query = query.Where(t => ((TagModel)(object)t).Tag.MatchesLQuery(specific));
                    //    break;
                    //case nameof(UserTagModel):
                    //    query = query.Where(ut => ((UserTagModel)(object)ut).Tag.MatchesLQuery(specific));
                    //    if (user != null) query = query.Where(ut => ((UserTagModel)(object)ut).User.Contains(user));
                    //    break;
                    //case nameof(UserModel):
                    //    if (user != null) query = query.Where(u => ((UserModel)(object)u).Username.Contains(user));
                    //    break;
                    case nameof(QuestionAnswerModel):
                        query.Include("QuestionData");
                        break;
                    default:
                        break;
                }
                if (filter != null)
                {
                    foreach (PropertyInfo property in typeof(TModel).GetProperties())
                    {
                        query = query.Where(item => property.GetValue(item).Equals(property.GetValue(filter)));
                    }
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                throw new Exception();
            }
        }

        public async Task Add(TModel model)
        {
            try
            {
                PropertyInfo? authorIdProperty = model.GetType().GetProperty("AuthorId");
                authorIdProperty?.SetValue(model, _auth.GetCurrentUser().Username);

                PropertyInfo? passwordProperty = model.GetType().GetProperty("Password");
                passwordProperty?.SetValue(model, _auth.CriptPassword((string)passwordProperty?.GetValue(model)));

                // check rights

                await _contextTable.AddAsync(model);
                await _context.SaveChangesAsync();
                await _audit.LogAuditTrail<TModel>(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                throw new Exception();
            }
        }

        public async Task Update(TModel model)
        {
            try
            {
                PropertyInfo? authorIdProperty = model.GetType().GetProperty("AuthorId");
                authorIdProperty?.SetValue(model, _auth.GetCurrentUser().Username);

                // Find the old model in the dataSetia
                PropertyInfo? idProperty = model.GetType().GetProperty("Id");
                if (idProperty == null) throw new InvalidOperationException("Model does not have an Id property.");

                object? idValue = idProperty.GetValue(model);
                if (idValue == null) throw new ArgumentException("Id value cannot be null.");

                TModel? oldModel = await _contextTable.FindAsync(idValue);

                if (oldModel == null) throw new Exception($"Entity with Id '{idValue}' not found in the dataSetia.");


                _contextTable.Entry(oldModel).CurrentValues.SetValues(model);
                await _context.SaveChangesAsync();
                await _audit.LogAuditTrail<TModel>(model, oldModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                throw new Exception();
            }
        }

        public async Task Delete(string id)
        {
            try
            {
                TModel? itemToDelete = await _contextTable.FindAsync(id);

                if (itemToDelete == null) throw new Exception();

                _contextTable.Remove(itemToDelete);
                await _context.SaveChangesAsync();

                await _audit.LogAuditTrail<TModel>(itemToDelete);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                throw new Exception();
            }
        }
    }
}