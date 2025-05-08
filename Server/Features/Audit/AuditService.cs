using Main.Data.Context;
using Main.Modules.Auth;
using System.Reflection;
using System.Text.Json;

namespace Main.Modules.Audit;

public interface IAuditService
{
    Task<AuditModel> LogAuditTrail<T>(T model, T? oldModel = default);
    Task<string> CompareObjects<T>(T obj1, T obj2);
}

public class AuditService(BaseContext context, ILogger<AuditService> logger, IAuthService auth) : IAuditService
{
    /// <summary>
    /// Logs the audit trail of a model.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="model"></param>
    /// <param name="oldModel"></param>
    /// <returns></returns>
    public async Task<AuditModel> LogAuditTrail<T>(T model, T? oldModel = default)
    {
        try
        {
            var newAudit = new AuditModel()
            {
                AuthorId = (await auth.GetCurrentUser())?.Id,
                Entity = typeof(T).FullName!,
                EntityId = typeof(T).GetProperties().FirstOrDefault()?.GetValue(model)?.ToString(),
                Payload = oldModel == null ? JsonSerializer.Serialize(model) : JsonSerializer.Serialize(CompareModels(oldModel, model))
            };
            await context.Set<AuditModel>().AddAsync(newAudit);
            await context.SaveChangesAsync();
            return newAudit;
        }
        catch (Exception ex) { logger.LogError(ex, GetType().FullName, ex.Message); throw; }
    }

    /// <summary>
    /// Compares two objects of the same type and returns a string representation of the differences.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj1"></param>
    /// <param name="obj2"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Compares two models of the same type and returns a dictionary of differences.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="oldModel"></param>
    /// <param name="newModel"></param>
    /// <returns></returns>
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
