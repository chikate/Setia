using Base.Gateways.Bases;
using Setia.Models.Base;
using Setia.Services.Interfaces;

namespace Setia.Controllers
{
    public class NotificationsController(ICRUD<NotificationModel> CRUD) : CRUDController<NotificationModel>(CRUD);
}