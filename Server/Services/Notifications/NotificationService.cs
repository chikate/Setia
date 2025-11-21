using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Services.Notifications;

public class NotificationService(BaseContext db, IHubContext<EventsHub> hub) : INotificationService
{
    public async Task<List<NotificationModel>> GetUnread(Guid userId)
    {
        return await db.Notifications
            .Where(n => n.To == userId && n.SeenDate == null)
            .OrderByDescending(n => n.ExecutionDate)
            .ToListAsync();
    }

    public async Task MarkAsRead(Guid notificationId)
    {
        await db.Notifications
            .Where(n => n.Id == notificationId)
            .ExecuteUpdateAsync(setters => setters.SetProperty(n => n.SeenDate, DateTimeOffset.UtcNow));
    }

    public async Task Send(NotificationModel notification)
    {
        db.Notifications.Add(notification);
        await db.SaveChangesAsync();

        await hub.Clients.User(notification.To.ToString()).SendAsync("ReceiveNotification", notification);
    }
}
