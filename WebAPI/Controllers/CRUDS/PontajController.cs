using Microsoft.AspNetCore.Mvc;
using Setia.Models;
using Setia.Services.Interfaces;

namespace Setia.Controllers.CRUDS
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class PontajController : CRUDController<PontajModel>
    {
        public PontajController(ICRUD<PontajModel> CRUD) : base(CRUD) { }
    }
}