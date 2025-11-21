using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Modules.Auth;
using Services.Notifications;

namespace Features.CRUDs;

public class UsersController(ICRUDService<UserModel> CRUD) : CRUDController<UserModel>(CRUD);
public class GroupsController(ICRUDService<GroupModel> CRUD) : CRUDController<GroupModel>(CRUD);
public class LinksController(ICRUDService<LinkModel> CRUD) : CRUDController<LinkModel>(CRUD);

[ApiController]
[Route("api/[controller]/[action]")]
public class NotificationsController(ICRUDService<NotificationModel> CRUD, INotificationService notificationService) : CRUDController<NotificationModel>(CRUD)
{
    [HttpGet]
    public async Task Send(NotificationModel notification) => await notificationService.Send(notification);
}

public class ParametersController(ICRUDService<ParameterModel> CRUD) : CRUDController<ParameterModel>(CRUD);
public class PostsController(ICRUDService<PostModel> CRUD) : CRUDController<PostModel>(CRUD);
public class QuestionAnswersController(ICRUDService<QuestionModel> CRUD) : CRUDController<QuestionModel>(CRUD);
//{
//[HttpGet]
//public async Task GetQuestionAnswereDistribution([FromQuery] Guid questionId)
//{
//        //var responseCounts = await _govContext.QuestionAnswers
//        //    .Where(q => q.QuestionId == questionId)
//        //    .GroupBy(q => q.AuthorId)
//        //    .Select(g => g.Count())
//        //    .ToListAsync();
//        return Ok(/*responseCounts*/);
//}
//}