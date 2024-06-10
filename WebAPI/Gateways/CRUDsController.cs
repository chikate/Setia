using Base.Gateways.Controllers;
using Setia.Models.Base;
using Setia.Models.Gov;
using Setia.Services.Interfaces;

namespace Setia.Controllers
{
    // Base
    public class TagsController(ICRUD<TagModel> CRUD) : CRUDController<TagModel>(CRUD);
    public class UsersController(ICRUD<UserModel> CRUD) : CRUDController<UserModel>(CRUD);
    public class NotificationsController(ICRUD<NotificationModel> CRUD) : CRUDController<NotificationModel>(CRUD);

    // Gov
    public class PontajController(ICRUD<PontajModel> CRUD) : CRUDController<PontajModel>(CRUD);
    public class QuestionsController(ICRUD<QuestionModel> CRUD) : CRUDController<QuestionModel>(CRUD);
    public class QuestionAnswersController(ICRUD<QuestionAnswerModel> CRUD) : CRUDController<QuestionAnswerModel>(CRUD);
    public class PostsController(ICRUD<PostModel> CRUD) : CRUDController<PostModel>(CRUD);
    public class UserCollectionController(ICRUD<UserCollectionModel> CRUD) : CRUDController<UserCollectionModel>(CRUD);
}