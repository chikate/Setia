using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Unhandled exception");

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 400;

        await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = "Internal Server Error" }), cancellationToken);

        return true;
    }
}
