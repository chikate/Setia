using Main.Data.Context;

namespace Main.Modules.Auth;

public class UserRightsMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, BaseContext db, IAuthService auth)
    {
        // Get the current user
        var user = await auth.GetCurrentUser();
        if (user == null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("User not authenticated");
            return;
        }

        // Determine API "right" using caller info
        string apiRight =
            context.GetEndpoint()?.Metadata.GetMetadata<Microsoft.AspNetCore.Routing.RouteNameMetadata>()?.RouteName ?? context.Request.Path.Value?.Trim('/').Replace("/", ".") ?? ""; // e.g., "api.users.get"

        // Admin bypass
        if (!user.Tags.Any(t => t.Contains("Dragos") || t.Contains("Role.Admin") || t.Contains(apiRight)))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync($"User '{user.Name}' does not have required right: {apiRight}");
            return;
        }

        await next(context);
    }
}
