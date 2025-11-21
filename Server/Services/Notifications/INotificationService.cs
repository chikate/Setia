using Data.Models;

namespace Services.Notifications;

public interface INotificationService
{
    Task<List<NotificationModel>> GetUnread(Guid userId);
    Task MarkAsRead(Guid notificationId);
    Task Send(NotificationModel notification);
}
