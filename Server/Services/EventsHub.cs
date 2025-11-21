using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Services;

[Authorize]
public class EventsHub(BaseContext db) : Hub
{
    public async Task SendNotification(object data) =>
        await Clients.All.SendAsync("ReceiveNotification", data);

    public async Task SendMessage(Guid toUserId, string message)
    {
        var userIdString = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
        {
            throw new HubException("Unauthorized");
        }
        
        var msg = new MessageModel
        {
            AuthorId = userId,
            To = toUserId,
            Message = message,
            ExecutionDate = DateTime.UtcNow,
            Attachments = new List<Guid>()
        };
        
        db.Messages.Add(msg);
        await db.SaveChangesAsync();

        await Clients.User(toUserId.ToString()).SendAsync("ReceiveMessage", msg);
        await Clients.Caller.SendAsync("ReceiveMessage", msg);
    }
}