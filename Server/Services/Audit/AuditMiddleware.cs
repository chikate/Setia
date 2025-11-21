using Modules.Auth;
using System.Text;
using System.Text.Json;

namespace Modules.Audit;

public class AuditMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IAuditService auditService, IAuthService authService)
    {
        context.Request.EnableBuffering();

        string? body = null;
        if (context.Request.ContentLength > 0 &&
            context.Request.ContentType != null &&
            context.Request.ContentType.Contains("application/json", StringComparison.OrdinalIgnoreCase))
        {
            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
            body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
        }

        UserModel? user = await authService.GetCurrentUser();

        if (user != null)
            await auditService.LogAuditTrail(new AuditModel
            {
                AuthorId = user.Id,
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
            });

        await next(context);
    }
}
