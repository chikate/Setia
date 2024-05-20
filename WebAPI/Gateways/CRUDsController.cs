using Setia.Models.Base;
using Setia.Models.Gov;
using Setia.Services.Interfaces;

namespace Setia.Controllers
{
    // Base
    public class TagsController(ICRUD<TagModel> CRUD) : CRUDController<TagModel>(CRUD);
    public class UsersController(ICRUD<UserModel> CRUD) : CRUDController<UserModel>(CRUD);
    public class UserTagsController(ICRUD<UserTagModel> CRUD) : CRUDController<UserTagModel>(CRUD);

    // Gov
    public class QuestionsController(ICRUD<QuestionModel> CRUD) : CRUDController<QuestionModel>(CRUD);
    public class QuestionAnswersController(ICRUD<QuestionAnswerModel> CRUD) : CRUDController<QuestionAnswerModel>(CRUD);
}