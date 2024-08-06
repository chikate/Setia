using Main.Data.Models.Base;
using Main.Services.Base.Interfaces;
using Main.Standards.Controllers;

namespace Main.APIs.Base
{
    public class NotificationsController(ICRUD<NotificationModel> CRUD) : CRUDController<NotificationModel>(CRUD);
}