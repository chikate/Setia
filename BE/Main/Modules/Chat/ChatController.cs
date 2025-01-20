using Main.Standards;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Main.Modules.Chat;

public class ChatController : StandardAPI
{
    #region Dependency Injection 
    private readonly ILogger<ChatController> _logger;
    private readonly List<HttpResponse> Clients = new();
    private readonly object Lock = new();

    public ChatController
    (
        ILogger<ChatController> logger
    )
    {
        _logger = logger;
    }
    #endregion

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromServices] SSEClientManager clientManager, [FromBody] string message)
    {
        // Step 1: Save the message to the database
        // Example (pseudo-code):
        // await _dbContext.Messages.AddAsync(new Message { Content = message, SenderId = 1 });
        // await _dbContext.SaveChangesAsync();

        // Step 2: Prepare the SSE message
        var messageBytes = Encoding.UTF8.GetBytes($"data: {{\"message\":\"{message}\",\"sender\":1}}\n\n");

        // Step 3: Broadcast to all connected clients
        foreach (var client in clientManager.GetClients())
            if (client.Body.CanWrite)
            {
                await client.Body.WriteAsync(messageBytes, 0, messageBytes.Length);
                await client.Body.FlushAsync();
            }

        return Ok();
    }
}