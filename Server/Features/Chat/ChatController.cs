using Microsoft.AspNetCore.Mvc;

namespace Main.Modules.Chat;

[Route("api/[controller]/[action]")]
public class ChatController(ILogger<ChatController> logger, IChatService chatService) : ControllerBase
{
    //[HttpPost]
    //public async Task<IActionResult> SendMessage([FromServices] SSEClientManager clientManager, [FromBody] MessageModel messageData)
    //{
    //    try
    //    {
    //        _logger.LogInformation("Sending");
    //        return Ok(await _chatService.SendMessage(clientManager, messageData));
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogInformation("Error sending");
    //        return BadRequest(ex.Message);
    //    }
    //}
}