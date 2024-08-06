using Main.Services.Base.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.Standards.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public abstract class CRUDController<TModel> : ControllerBase
    {
        private readonly ICRUD<TModel> _CRUD;
        public CRUDController(ICRUD<TModel> CRUD) { _CRUD = CRUD; }

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
            try
            {
                foreach (TModel model in models) await _CRUD.Add(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] List<TModel> models)
        {
            try
            {
                foreach (TModel model in models) await _CRUD.Update(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Error message: " + ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(List<string> ids)
        {

            try
            {
                foreach (string id in ids) await _CRUD.Delete(id);
                return Ok($"[{ids}] were permanently deleted");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error message: " + ex.Message);
            }
        }
    }
}
