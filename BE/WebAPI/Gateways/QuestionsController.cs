using Base.Gateways.Bases;
using Setia.Models.Gov;
using Setia.Services.Interfaces;

namespace Setia.Controllers
{
    public class QuestionsController(ICRUD<QuestionModel> CRUD) : CRUDController<QuestionModel>(CRUD);
}