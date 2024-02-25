using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Setia.Models;
using Setia.Services.Interfaces;

namespace Setia.Controllers.CRUDS
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]/[action]")]
    public class RolesController : ControllerBase
    {
        private readonly ICRUD<RoleModel> _CRUD;

        public RolesController(ICRUD<RoleModel> CRUD) { _CRUD = CRUD; }

        [HttpPost]
        public async Task<bool> Add([FromBody] RoleModel model)
        {
            return await _CRUD.Add(model);
        }

        [HttpPut]
        public async Task<bool> Update([FromBody] RoleModel model)
        {
            return await _CRUD.Update(model);
        }

        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            return await _CRUD.Delete(id);
        }

        [HttpGet]
        public async Task<IEnumerable<RoleModel>> GetAll([FromBody] RoleModel? filter = null)
        {
            return await _CRUD.GetAll(filter);
        }
    }
}