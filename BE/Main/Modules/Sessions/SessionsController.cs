using Main.Standards;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Main.Modules.Sessions;

public class SessionsController : StandardAPI
{
    #region Dependency Injection 
    private readonly ILogger<SessionsController> _logger;
    private readonly List<HttpResponse> Clients = new();
    private readonly object Lock = new();

    public SessionsController
    (
        ILogger<SessionsController> logger
    )
    {
        _logger = logger;
    }
    #endregion

    [HttpPost]
    public async Task MousePosition([FromServices] SSEClientManager clientManager, int x, int y)
    {
        var bytes = Encoding.UTF8.GetBytes($"[{{\"x\":{x},\"y\":{y}}}]\n\n");

        var disconnectedClients = new List<HttpResponse>();

        foreach (var client in clientManager.GetClients())
        {
            try
            {
                await client.Body.WriteAsync(bytes, 0, bytes.Length);
                await client.Body.FlushAsync();
            }
            catch
            {
                disconnectedClients.Add(client);
            }
        }

        foreach (var client in disconnectedClients)
        {
            clientManager.RemoveClient(client);
        }
    }
}