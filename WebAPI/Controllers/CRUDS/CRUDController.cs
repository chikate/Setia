using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Setia.Services.Interfaces;

namespace Setia.Controllers.CRUDS
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]/[action]")]
    public class CRUDController<T> : ControllerBase
    {
        private readonly ICRUD<T> _CRUD;

        public CRUDController(ICRUD<T> CRUD) { _CRUD = CRUD; }

        [HttpGet]
        public IEnumerable<T> GetAll([FromQuery] T? filter)
        {
            return _CRUD.GetAll(filter);
        }

        [HttpPost]
        public async Task<bool> Add([FromBody] T model)
        {
            return await _CRUD.Add(model);
        }

        [HttpPut]
        public async Task<bool> Update([FromBody] T model)
        {
            return await _CRUD.Update(model);
        }

        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            return await _CRUD.Delete(id);
        }
    }
}