using Main.Data.Contexts;
using Main.Modules.Auth;
using System.Reflection;
using System.Text.Json;

namespace Main.Modules.Audit;

public interface IAuditService
{
    Task<string> CompareObjects<T>(T obj1, T obj2);
    Task LogAuditTrail<T>(T model, T? oldModel = default);
}

public class AuditService : IAuditService
{
    #region Dependency Injection 
    private readonly AuditContext _auditContext;
    private readonly ILogger<AuditService> _logger;
    private readonly IAuthService _auth;

    public AuditService
    (
        AuditContext auditContext,
        ILogger<AuditService> logger,
        IAuthService auth
    )
    {
        _auditContext = auditContext;
        _logger = logger;
        _auth = auth;
    }
    #endregion

    public async Task LogAuditTrail<T>(T model, T? oldModel = default)
    {
        try
        {
            await _auditContext.Set<AuditModel>().AddAsync(new AuditModel()
            {
                AuthorId = (await _auth.GetCurrentUser())?.Id,
                Entity = typeof(T).FullName!,
                EntityId = typeof(T).GetProperties().FirstOrDefault()?.GetValue(model)?.ToString(),
                Payload = oldModel == null ? JsonSerializer.Serialize(model) : JsonSerializer.Serialize(CompareModels(oldModel, model))
            });
            await _auditContext.SaveChangesAsync();
        }
        catch (Exception ex) { _logger.LogError(ex, GetType().FullName, ex.Message); throw; }
    }
    public async Task<string> CompareObjects<T>(T obj1, T obj2)
    {
        Dictionary<string, string> differences = [];

        foreach (PropertyInfo property in typeof(T).GetProperties())
        {
            string value1 = property.GetValue(obj1)?.ToString()!;
            string value2 = property.GetValue(obj2)?.ToString()!;

            if (value1 != value2)
                differences.Add(property.Name, $"Object 1 value: {value1}, Object 2 value: {value2}");
        }

        await Task.CompletedTask;
        return differences.ToString()!;
    }
    private IDictionary<string, Dictionary<string, string>> CompareModels<T>(T oldModel, T newModel)
    {
        Dictionary<string, Dictionary<string, string>> differences = [];

        foreach (PropertyInfo property in typeof(T).GetProperties())
        {
            string existingValue = property.GetValue(oldModel)?.ToString()!;
            string updatedValue = property.GetValue(newModel)?.ToString()!;

            if (existingValue != updatedValue)
            {
                Dictionary<string, string> propertyDifference = new()
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
