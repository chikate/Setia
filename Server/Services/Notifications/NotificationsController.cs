using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Modules.Auth;

namespace Services.Notifications;

[ApiController]
[Route("api/[controller]/[action]")]
public class NotificationsController(INotificationService service, IAuthService auth) : ControllerBase
{
    [HttpGet]
    public async Task<List<NotificationModel>> GetUnread()
        => await service.GetUnread((await auth.GetCurrentUser()).Id);

    [HttpPost]
    public async Task MarkAsRead(Guid id)
        => await service.MarkAsRead(id);
}
