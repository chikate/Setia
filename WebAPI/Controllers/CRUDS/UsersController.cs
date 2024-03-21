using Microsoft.AspNetCore.Mvc;
using Setia.Models;
using Setia.Services.Interfaces;

namespace Setia.Controllers.CRUDS
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class UsersController : CRUDController<UserModel>
    {
        public UsersController(ICRUD<UserModel> CRUD) : base(CRUD) { }
    }
}