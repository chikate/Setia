using Base.Gateways.Bases;
using Setia.Models.Base;
using Setia.Services.Interfaces;

namespace Setia.Controllers
{
    public class TagsController(ICRUD<TagModel> CRUD) : CRUDController<TagModel>(CRUD);
}