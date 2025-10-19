using Main.Data.Context;
using Main.Modules.Auth;
using Main.Services;
using Microsoft.AspNetCore.SignalR;
using System.Reflection;
using System.Text.Json;

namespace Main.Modules.Audit;

public interface IAuditService
{
    Task<AuditModel> LogAuditTrail<T>(T model, T? oldModel = default);
    Task<string> CompareObjects<T>(T obj1, T obj2);
}

public class AuditService(BaseContext context, IAuthService auth, IHubContext<EventsHub> auditHUBContext) : IAuditService
{
    public async Task<AuditModel> LogAuditTrail<T>(T model, T? oldModel = default)
    {
        var audit = model as AuditModel ?? new AuditModel
        {
            AuthorId = (await auth.GetCurrentUser())?.Id,
            Entity = typeof(T).FullName!,
            EntityId = typeof(T).GetProperties().FirstOrDefault()?.GetValue(model)?.ToString(),
            Payload = oldModel == null
                ? JsonSerializer.Serialize(model)
                : JsonSerializer.Serialize(CompareModels(oldModel, model))
        };

        await context.AddAsync(audit);
        await context.SaveChangesAsync();
        await auditHUBContext.Clients.All.SendAsync("ReceiveAudit", audit);
        return audit;
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
