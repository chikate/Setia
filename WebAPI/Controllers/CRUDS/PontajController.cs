using Base.Controllers;
using Microsoft.AspNetCore.Mvc;
using Setia.Models;
using Setia.Services.Interfaces;

namespace Setia.Controllers.CRUDS
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class PontajController(ICRUD<PontajModel> CRUD) : CRUDController<PontajModel>(CRUD) { }
}