using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Setia.Models;
using Setia.Services.Interfaces;

namespace Setia.Controllers.CRUDS
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]/[action]")]
    public class PontajController : CRUDController<PontajModel>
    {
        public PontajController(ICRUD<PontajModel> CRUD) : base(CRUD) { }
    }
}