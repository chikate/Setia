using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Setia.Models;
using Setia.Services.Interfaces;

namespace Setia.Controllers.CRUDS
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]/[action]")]
    public class PontajController : ControllerBase
    {
        private readonly ICRUD<PontajModel> _CRUD;

        public PontajController(ICRUD<PontajModel> CRUD) { _CRUD = CRUD; }

        [HttpPost]
        public async Task<bool> Add([FromBody] PontajModel model)
        {
            return await _CRUD.Add(model);
        }

        [HttpPut]
        public async Task<bool> Update([FromBody] PontajModel model)
        {
            return await _CRUD.Update(model);
        }

        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            return await _CRUD.Delete(id);
        }

        [HttpGet]
        public async Task<IEnumerable<PontajModel>> GetAll([FromBody] PontajModel? filter = null)
        {
            return await _CRUD.GetAll(filter);
        }
    }
}