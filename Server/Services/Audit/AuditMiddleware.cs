using Main.Modules.Auth;
using System.Text;
using System.Text.Json;

namespace Main.Modules.Audit;

public class AuditMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IAuditService auditService, IAuthService authService)
    {
        context.Request.EnableBuffering();

        string? body = null;
        if (context.Request.ContentLength > 0 && context.Request.ContentType?.Contains("application/json") == true)
        {
            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
            body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
        }

        await next(context);

        _ = Task.Run(async () =>
        {
            try
            {
                AuditModel audit = new AuditModel
                {
                    AuthorId = (await authService.GetCurrentUser())?.Id,
                    Entity = context.Request.Path,
                    EntityId = Guid.NewGuid().ToString(),
                    Payload = JsonSerializer.Serialize(new
                    {
                        context.Request.Method,
                        context.Request.Path,
                        context.Request.QueryString,
                        Body = body,
                        Timestamp = DateTime.UtcNow
                    })
                };

                await auditService.LogAuditTrail(audit);
            }
            catch (Exception ex)
            {
                // Never break the request if audit fails
                Console.WriteLine($"[AUDIT ERROR] {ex.Message}");
            }
        });
    }
}
