using Main.Data.Models;
using Main.Modules.Auth;

namespace Main.Features.CRUDs;

public class PostsController(ICRUDService<PostModel> CRUD) : CRUDController<PostModel>(CRUD);
public class NotificationsController(ICRUDService<NotificationModel> CRUD) : CRUDController<NotificationModel>(CRUD);
public class LinksController(ICRUDService<LinkModel> CRUD) : CRUDController<LinkModel>(CRUD);
public class UsersController(ICRUDService<UserModel> CRUD) : CRUDController<UserModel>(CRUD);
public class CommunityController(ICRUDService<GroupModel> CRUD) : CRUDController<GroupModel>(CRUD);
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