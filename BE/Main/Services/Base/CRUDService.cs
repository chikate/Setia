using Main.Data.Models;
using Main.Services.Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Main.Services.Base
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
                // Step 1: Start with the base query, excluding "Deleted" tags.
                IQueryable<TModel> query = _dbData
                    .Where(e => !e.Tags.Any(t => t.Contains("Deleted")))
                    .AsNoTracking();

                // Step 2: Include necessary navigation properties based on ForeignKey attributes.
                foreach (PropertyInfo property in typeof(TModel).GetProperties().Where(p => p.GetCustomAttributes(typeof(ForeignKeyAttribute), false).Any()))
                {
                    query = query.Include(
                        property.Name.EndsWith("Id")
                            ? property.Name.Substring(0, property.Name.Length - 2) + "Data"
                            : property.Name + "Data"
                    );
                }

                // Step 3: Apply filters if the filter object is provided.
                if (filter != null)
                {
                    foreach (PropertyInfo property in typeof(TModel).GetProperties())
                    {
                        object? filterValue = property.GetValue(filter);
                        if (filterValue != null && !string.IsNullOrEmpty(filterValue.ToString()))
                        {
                            query = query.Where(e => property.GetValue(e) != null && property.GetValue(e).Equals(filterValue));
                        }
                    }
                }

                // Step 4: Return the filtered and processed list.
                return await query.ToListAsync();
            }
            catch (Exception ex) { _logger.LogError(ex.Message, GetType().FullName); throw; }
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
                await _audit.LogAuditTrail(model);
                return model;
            }
            catch (Exception ex) { _logger.LogError(ex.Message, GetType().FullName); throw; }
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
            catch (Exception ex) { _logger.LogError(ex.Message, GetType().FullName); throw; }
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
                    await _audit.LogAuditTrail(itemToDelete);
                }
            }
            catch (Exception ex) { _logger.LogError(ex.Message, GetType().FullName); throw; }
        }
    }
}