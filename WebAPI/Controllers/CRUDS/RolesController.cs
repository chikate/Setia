using Microsoft.AspNetCore.Mvc;
using Setia.Models;
using Setia.Services.Interfaces;

namespace Setia.Controllers.CRUDS
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class RolesController : CRUDController<TagModel>
    {
        public RolesController(ICRUD<TagModel> CRUD) : base(CRUD) { }
    }
}