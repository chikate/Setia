using Base.Gateways.Bases;
using Setia.Models.Base;
using Setia.Services.Interfaces;

namespace Setia.Controllers
{
    public class UsersController(ICRUD<UserModel> CRUD) : CRUDController<UserModel>(CRUD);
}