using Base.Controllers;
using Microsoft.AspNetCore.Mvc;
using Setia.Models;
using Setia.Services.Interfaces;

namespace Setia.Controllers.CRUDS
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class TagsController(ICRUD<TagModel> CRUD) : CRUDController<TagModel>(CRUD) { }
}