using Microsoft.EntityFrameworkCore;
using Setia.Data;
using Setia.Services.Interfaces;

namespace Setia.Controllers
{
    public class CRUDService<T> : ICRUD<T> where T : class
    {
        private readonly SetiaContext _context;
        private readonly DbSet<T> _dbTable;
        private readonly ILogger<CRUDService<T>> _logger;
        private readonly IAudit _audit;
        private readonly IAuth _auth;

        public CRUDService
        (
            SetiaContext context,
            ILogger<CRUDService<T>> logger,
            IAudit audit,
            IAuth auth
        )
        {
            _context = context;
            _dbTable = context.Set<T>();
            _logger = logger;
            _audit = audit;
            _auth = auth;
        }

        public IEnumerable<T> Get(T? filter)
        {
            try
            {
                return ApplyFilter(_dbTable.AsQueryable(), filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return [];
            }
        }

        async Task<bool> ICRUD<T>.Add(T model)
        {
            try
            {
                model.GetType().GetProperty("Author_Id")?.SetValue(model, await _auth.GetCurrentUserId());

                // check rights

                model.GetType().GetProperty("Id")?.SetValue(model, 0);

                await _dbTable.AddAsync(model);
                await _context.SaveChangesAsync();

                await _audit.LogAuditTrail<T>(model);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return false;
            }
        }

        async Task<bool> ICRUD<T>.Update(T model)
        {
            try
            {
                // Set the Author_Id property of the model
                var authorIdProperty = model.GetType().GetProperty("Author_Id");
                if (authorIdProperty != null) authorIdProperty.SetValue(model, await _auth.GetCurrentUserId());

                // Find the old model in the database
                var idProperty = model.GetType().GetProperty("Id");
                if (idProperty == null) throw new InvalidOperationException("Model does not have an Id property.");

                var idValue = idProperty.GetValue(model);
                if (idValue == null) throw new ArgumentException("Id value cannot be null.");

                var oldModel = await _dbTable.FindAsync(idValue);

                if (oldModel == null) throw new Exception($"Entity with Id '{idValue}' not found in the database.");

                // Update the context with the new model
                _dbTable.Entry(oldModel).CurrentValues.SetValues(model);
                await _context.SaveChangesAsync();

                // Log audit trail
                await _audit.LogAuditTrail<T>(model, oldModel);

                return true; // Operation succeeded
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return false; // Operation failed
            }
        }

        async Task<bool> ICRUD<T>.Delete(int id)
        {
            try
            {
                var itemToDelete = await _dbTable.FindAsync(id);

                if (itemToDelete == null) return false;

                _dbTable.Remove(itemToDelete);
                await _context.SaveChangesAsync();

                await _audit.LogAuditTrail<T>(itemToDelete);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return false;
            }
        }
        private IQueryable<T> ApplyFilter(IQueryable<T> query, T? filter)
        {
            if (filter != null)
            {
                foreach (var property in typeof(T).GetProperties())
                {
                    query = query.Where(item => property.GetValue(item).Equals(property.GetValue(filter)));
                }
            }
            return query;
        }
    }
}