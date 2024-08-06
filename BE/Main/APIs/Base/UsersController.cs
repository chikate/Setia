using Main.Data.Models.Base;
using Main.Services.Base.Interfaces;
using Main.Standards.Controllers;

namespace Main.APIs.Base
{
    public class UsersController(ICRUD<UserModel> CRUD) : CRUDController<UserModel>(CRUD);
}