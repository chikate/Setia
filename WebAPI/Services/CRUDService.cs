using Microsoft.EntityFrameworkCore;
using Setia.Models.Structs;
using Setia.Services.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Setia.Controllers
{
    public class CRUDService<TModel, TContext> : ICRUD<TModel>
        where TModel : DefinitionStruct
        where TContext : DbContext
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

        public async Task<IEnumerable<TModel>> Get(List<TModel>? filter = default)
        {
            try
            {
                IQueryable<TModel> query = _contextTable.AsNoTracking().AsQueryable();
                query = query.Where(e => !e.Deleted);

                //foreach (string tag in await _auth.GetUserTags(""))
                //if (!(await _auth.CheckUserRights(["Dragos"])).Any(c => c == "Dragos"))
                //{
                //    query = query.Where(e => e.User == _auth.GetCurrentUser()?.Username);
                //}

                #region test
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

                foreach (PropertyInfo property in typeof(TModel).GetProperties())
                {
                    if (property.GetCustomAttributes(typeof(ForeignKeyAttribute), false).Any())
                        query = query.Include(property.Name + "Data");

                    //if (filter != null)
                    //    query = query.Where(e => property.GetValue(e).Equals(property.GetValue(filter)));
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                throw new Exception();
            }
        }

        public async Task Add(List<TModel> models)
        {
            try
            {
                await _auth.CheckUserRights([$"{typeof(TModel).Name.Replace("Controller", "").Replace("Model", "")}.Add"]);
                foreach (TModel model in models)
                {
                    model.GetType().GetProperty("Author")?.SetValue(model, _auth.GetCurrentUser()?.Username);
                    //model.GetType().GetProperty("AuthorData")?.SetValue(model, _auth.GetCurrentUser());
                    PropertyInfo? passwordProperty = model.GetType().GetProperty("Password");
                    passwordProperty?.SetValue(model, _auth.CriptPassword((string)passwordProperty?.GetValue(model)));

                    await _contextTable.AddAsync(model);
                }
                await _context.SaveChangesAsync();
                foreach (TModel model in models) await _audit.LogAuditTrail<TModel>(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                throw new Exception();
            }
        }

        public async Task Update(List<TModel> models)
        {
            try
            {
                await _auth.CheckUserRights([$"{typeof(TModel).Name.Replace("Controller", "").Replace("Model", "")}.Update"]);
                List<TModel>? oldModels = new();
                foreach (TModel model in models)
                {
                    //PropertyInfo? userProperty = model.GetType().GetProperty("User");
                    //if (userProperty?.GetValue(model) == null) userProperty?.SetValue(model, _auth.GetCurrentUser());
                    //PropertyInfo? usernameProperty = model.GetType().GetProperty("Username");
                    //if (usernameProperty?.GetValue(model) == null) usernameProperty?.SetValue(model, _auth.GetCurrentUser().Username);
                    //PropertyInfo? AuthorUsernameProperty = model.GetType().GetProperty("AuthorUsername");
                    //AuthorUsernameProperty?.SetValue(model, _auth.GetCurrentUser().Username);

                    PropertyInfo? idProperty = model.GetType().GetProperties()[0]; if (idProperty == null) throw new InvalidOperationException("Model does not have an Id property.");

                    //var idValue = idProperty.GetValue(model); if (idValue == null) throw new ArgumentException("Id value cannot be null.");
                    TModel? oldModel = await _contextTable.FindAsync(idProperty.GetValue(model)); // if (oldModel == null) throw new Exception($"Entity with Id '{idValue}' not found in the dataSetia.");
                    if (oldModel != null)
                    {
                        oldModels.Add(oldModel);
                        _contextTable.Entry(oldModel).CurrentValues.SetValues(model);
                    }
                }
                await _context.SaveChangesAsync();
                for (int i = 0; i < models.Count; i++) await _audit.LogAuditTrail(models[i], oldModels[i]);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                throw new Exception();
            }
        }

        public async Task Delete(List<string> ids)
        {
            try
            {
                await _auth.CheckUserRights([$"{typeof(TModel).Name.Replace("Controller", "").Replace("Model", "")}.Delete"]);
                List<TModel>? itemsToDelete = new();
                foreach (string id in ids)
                {
                    TModel? itemToDelete = await _contextTable.FindAsync(id);
                    if (itemToDelete != null)
                    {
                        itemsToDelete.Add(itemToDelete);
                        _contextTable.Remove(itemToDelete);
                    }
                }
                await _context.SaveChangesAsync();
                foreach (TModel deletedItem in itemsToDelete) await _audit.LogAuditTrail<TModel>(deletedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                throw new Exception();
            }
        }
    }
}