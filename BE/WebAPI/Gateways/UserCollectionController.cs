using Base.Gateways.Bases;
using Setia.Models.Gov;
using Setia.Services.Interfaces;

namespace Setia.Controllers
{
    public class UserCollectionController(ICRUD<UserCollectionModel> CRUD) : CRUDController<UserCollectionModel>(CRUD);
}