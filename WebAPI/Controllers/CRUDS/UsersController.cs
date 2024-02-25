using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Setia.Models;
using Setia.Services.Interfaces;

namespace Setia.Controllers.CRUDS
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly ICRUD<UserModel> _CRUD;

        public UsersController(ICRUD<UserModel> CRUD) { _CRUD = CRUD; }

        [HttpPost]
        public async Task<bool> Add([FromBody] UserModel model)
        {
            return await _CRUD.Add(model);
        }

        [HttpPut]
        public async Task<bool> Update([FromBody] UserModel model)
        {
            return await _CRUD.Update(model);
        }

        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            return await _CRUD.Delete(id);
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetAll([FromBody] UserModel? filter = null)
        {
            return await _CRUD.GetAll(filter);
        }
    }
}