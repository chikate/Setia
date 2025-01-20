using Main.Modules.Audit;
using Main.Standards;
using Microsoft.AspNetCore.Mvc;

public class SSEController : StandardAPI
{
    #region Dependency Injection 
    private readonly AuditContext _auditContext;
    private readonly ILogger<SSEController> _logger;

    public SSEController
    (
        AuditContext auditContext,
        ILogger<SSEController> logger
    )
    {
        _auditContext = auditContext;
        _logger = logger;
    }
    #endregion

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
