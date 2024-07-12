using Base.Gateways.Bases;
using Setia.Models.Gov;
using Setia.Services.Interfaces;

namespace Setia.Controllers
{
    public class PostsController(ICRUD<PostModel> CRUD) : CRUDController<PostModel>(CRUD);
}