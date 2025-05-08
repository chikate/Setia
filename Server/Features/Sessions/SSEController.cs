using Main.Data.Context;
using Main.Modules.Sessions;
using Microsoft.AspNetCore.Mvc;

public class SSEController(BaseContext auditContext, ILogger<SSEController> logger) : ControllerBase
{
    [HttpGet]
    public async Task StreamSSE([FromServices] SSEClientManager clientManager)
    {
        Response.ContentType = "text/event-stream";
        Response.Headers.Append("Cache-Control", "no-cache");
        Response.Headers.Append("Connection", "keep-alive");

        clientManager.AddClient(Response);

        try
        {
            while (!HttpContext.RequestAborted.IsCancellationRequested)
            {
                await Task.Delay(1000);
            }
        }
        finally
        {
            clientManager.RemoveClient(Response);
        }
    }
}
