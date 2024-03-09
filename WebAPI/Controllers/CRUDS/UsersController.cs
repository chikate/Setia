using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Setia.Models;
using Setia.Services.Interfaces;

namespace Setia.Controllers.CRUDS
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]/[action]")]
    public class UsersController : CRUDController<UserModel>
    {
        public UsersController(ICRUD<UserModel> CRUD) : base(CRUD) { }
    }
}