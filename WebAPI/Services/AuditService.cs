using Setia.Contexts.Base;
using Setia.Models.Base;
using Setia.Services.Interfaces;
using System.Reflection;
using System.Text.Json;

namespace Setia.Services
{
    public class AuditService : IAudit
    {
        private readonly BaseContext _context;
        private readonly ILogger<AuditService> _logger;
        private readonly IAuth _auth;

        public AuditService
        (
            BaseContext context,
            ILogger<AuditService> logger,
            IAuth auth
        )
        {
            _context = context;
            _logger = logger;
            _auth = auth;
        }

        public async Task LogAuditTrail<T>(T model, T? oldModel = default)
        {
            try
            {
                AuditModel auditModel = new AuditModel
                {
                    AuthorId = _auth.GetCurrentUser().Username,
                    Author = _auth.GetCurrentUser(),
                    Entity = typeof(T).FullName ?? "",
                    EntityId = GetEntityId(model),
                    Payload = oldModel == null ? JsonSerializer.Serialize(model) : JsonSerializer.Serialize(CompareModels(oldModel, model))
                };

                await _context.Audit.AddAsync(auditModel);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
            }
        }

        public string CompareObjects<T>(T obj1, T obj2)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            Dictionary<string, string> differences = [];

            foreach (PropertyInfo property in properties)
            {
                string value1 = property.GetValue(obj1)?.ToString() ?? "";
                string value2 = property.GetValue(obj2)?.ToString() ?? "";

                if (value1 != value2)
                {
                    differences.Add(property.Name, $"Object 1 value: {value1}, Object 2 value: {value2}");
                }
            }

            return differences.ToString() ?? "";
        }

        private int GetEntityId<T>(T model)
        {
            // Assuming the entity has a property named "Id"
            PropertyInfo? property = typeof(T).GetProperty("Id");
            if (property != null)
            {
                // Extract the value of the "Id" property
                object? value = property.GetValue(model);
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
            Dictionary<string, Dictionary<string, string>> differences = [];

            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                string existingValue = property.GetValue(oldModel)?.ToString() ?? "";
                string updatedValue = property.GetValue(newModel)?.ToString() ?? "";

                if (existingValue != updatedValue)
                {
                    Dictionary<string, string> propertyDifference = new Dictionary<string, string>
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
