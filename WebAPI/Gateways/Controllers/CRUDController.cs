using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Setia.Services.Interfaces;

namespace Base.Gateways.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class CRUDController<TModel> : ControllerBase
    {
        private readonly ICRUD<TModel> _CRUD;
        //private readonly IAuth _auth;
        public CRUDController
        (
            ICRUD<TModel> CRUD
        //IAuth auth
        )
        {
            _CRUD = CRUD;
            //_auth = auth;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TModel>>> Get(/*[FromQuery] List<TModel>? filters = default*/)
        {
            try
            {
                // await _auth.CheckUserRights([$"{typeof(TModel).Name.Replace("Controller", "").Replace("Model", "")}.Add"]);
                return Ok(await _CRUD.Get());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] List<TModel> models)
        {
            try { return Ok(await _CRUD.Add(models)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] List<TModel> models)
        {
            try { return Ok(await _CRUD.Update(models)); }
            catch (Exception ex) { return BadRequest("Error message: " + ex.Message); }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(List<string> ids)
        {
            try
            {
                await _CRUD.Delete(ids);
                return Ok($"[{ids}] were permanently deleted");
            }
            catch (Exception ex)
            {
                return BadRequest("Error message: " + ex.Message);
            }
        }
    }
}
