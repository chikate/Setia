using Base;
using Microsoft.EntityFrameworkCore;
using Setia.Services.Interfaces;
using System.Reflection;

namespace Setia.Controllers
{
    public class CRUDService<T> : ICRUD<T> where T : class
    {
        private readonly BaseContext _context;
        private readonly DbSet<T> _dbTable;
        private readonly ILogger<CRUDService<T>> _logger;
        private readonly IAudit _audit;
        private readonly IAuth _auth;

        public CRUDService
        (
            BaseContext context,
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
                model.GetType().GetProperty("AuthorId")?.SetValue(model, _auth.GetCurrentUser().Id);

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
                // Set the AuthorId property of the model
                PropertyInfo? authorIdProperty = model.GetType().GetProperty("AuthorId");
                if (authorIdProperty != null) authorIdProperty.SetValue(model, _auth.GetCurrentUser().Id);

                // Find the old model in the database
                PropertyInfo? idProperty = model.GetType().GetProperty("Id");
                if (idProperty == null) throw new InvalidOperationException("Model does not have an Id property.");

                object? idValue = idProperty.GetValue(model);
                if (idValue == null) throw new ArgumentException("Id value cannot be null.");

                T? oldModel = await _dbTable.FindAsync(idValue);

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
                T? itemToDelete = await _dbTable.FindAsync(id);

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
                foreach (PropertyInfo property in typeof(T).GetProperties())
                {
                    query = query.Where(item => property.GetValue(item).Equals(property.GetValue(filter)));
                }
            }
            return query;
        }
    }
}