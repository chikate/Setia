using Main.Data.Models;
using Main.Services.Interfaces;
using Main.Standards.Controllers;

namespace Main.APIs.Base;

public class NotificationsController(ICRUD<NotificationModel> CRUD) : CRUDController<NotificationModel>(CRUD);