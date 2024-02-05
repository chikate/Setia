using Microsoft.EntityFrameworkCore;
using Setia.Data;
using Setia.Models;
using Setia.Services.Interfaces;
using System.Reflection;
using System.Text.Json;

namespace Setia.Services
{
    public class AuditService : IAudit
    {
        private readonly SetiaContext _context;
        private readonly ILogger<AuditService> _logger;
        private readonly IAuth _auth;

        public AuditService
        (
            SetiaContext context,
            ILogger<AuditService> logger,
            IAuth auth
        )
        {
            _context = context;
            _logger = logger;
            _auth = auth;
        }

        public async Task LogAuditTrail<T>(T model, T oldModel = default)
        {
            try
            {
                var auditModel = new AuditModel
                {
                    Id_Executioner = await _auth.GetCurrentUserId(),
                    Entity = typeof(T).FullName,
                    Id_Entity = GetEntityId(model),
                    Payload = oldModel == null ? JsonSerializer.Serialize(model) : JsonSerializer.Serialize(CompareModels(oldModel, model))
                };

                await Add(auditModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
            }
        }

        public async Task<IEnumerable<AuditModel>> GetAll()
        {
            try
            {
                return await _context.Audit.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return [];
            }
            finally
            {
                _logger.LogInformation(this.GetType().FullName);
            }
        }

        public IEnumerable<AuditModel> GetAllWithFilter(AuditModel filter)
        {
            try
            {
                var query = _context.Audit.AsQueryable();
                var result = AddFilter(query, filter);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return [];
            }
            finally
            {
                _logger.LogInformation(this.GetType().FullName);
            }
        }

        public async Task<bool> Add(AuditModel model)
        {
            try
            {
                await _context.Audit.AddAsync(model);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return false;
            }
            finally
            {
                _logger.LogInformation(this.GetType().FullName);
            }
        }

        public async Task<bool> Update(AuditModel model)
        {
            try
            {
                _context.Audit.Update(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return false;
            }
            finally
            {
                _logger.LogInformation(this.GetType().FullName);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var auditToDelete = await _context.Audit.FindAsync(id);
                if (auditToDelete != null)
                {
                    _context.Audit.Remove(auditToDelete);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return false;
            }
            finally
            {
                _logger.LogInformation(this.GetType().FullName);
            }
        }

        public string CompareObjects<T>(T obj1, T obj2)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            var differences = new Dictionary<string, string>();

            foreach (PropertyInfo property in properties)
            {
                string value1 = property.GetValue(obj1)?.ToString();
                string value2 = property.GetValue(obj2)?.ToString();

                if (value1 != value2)
                {
                    differences.Add(property.Name, $"Object 1 value: {value1}, Object 2 value: {value2}");
                }
            }

            return differences.ToString();
        }

        private static IQueryable<AuditModel> AddFilter(IQueryable<AuditModel> query, AuditModel filter)
        {
            if (filter != null)
            {
                foreach (var property in typeof(AuditModel).GetProperties())
                {
                    query = query.Where(item => property.GetValue(item).Equals(property.GetValue(filter)));
                }
            }
            return query;
        }

        private int GetEntityId<T>(T model)
        {
            // Assuming the entity has a property named "Id"
            var property = typeof(T).GetProperty("Id");
            if (property != null)
            {
                // Extract the value of the "Id" property
                var value = property.GetValue(model);
                if (value != null && int.TryParse(value.ToString(), out int id))
                {
                    return id;
                }
            }

            // Default return value if the ID couldn't be retrieved
            return -1;
        }

        private IDictionary<string, Dictionary<string, string>> CompareModels<T>(T oldModel, T newModel)
        {
            var differences = new Dictionary<string, Dictionary<string, string>>();

            foreach (var property in typeof(T).GetProperties())
            {
                var existingValue = property.GetValue(oldModel)?.ToString();
                var updatedValue = property.GetValue(newModel)?.ToString();

                if (existingValue != updatedValue)
                {
                    var propertyDifference = new Dictionary<string, string>
                    {
                        { "old", existingValue ?? "null" },
                        { "new", updatedValue ?? "null" }
                    };

                    differences[property.Name] = propertyDifference;
                }
            }

            return differences;
        }
    }
}
