using Main.Data.Models;
using Main.Services.Base.Interfaces;
using Main.Standards.Controllers;

namespace Main.APIs.Gov
{
    public class PontajController(ICRUD<IntervalModel> CRUD) : CRUDController<IntervalModel>(CRUD);
    public class PostsController(ICRUD<PostModel> CRUD) : CRUDController<PostModel>(CRUD);
    public class QuestionsController(ICRUD<QuestionModel> CRUD) : CRUDController<QuestionModel>(CRUD);
}