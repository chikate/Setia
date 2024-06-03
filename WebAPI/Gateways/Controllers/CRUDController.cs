using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Setia.Services.Interfaces;
using System.Reflection;
using System.Text.Json;

namespace Base.Gateways.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class CRUDController<TModel> : ControllerBase
    {
        private readonly ICRUD<TModel> _CRUD;
        public CRUDController(ICRUD<TModel> CRUD) { _CRUD = CRUD; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TModel>>> Get(/*[FromQuery] List<TModel>? filters = default*/)
        {
            try
            {
                return Ok(await _CRUD.Get());
            }
            catch (Exception ex)
            {
                return BadRequest("Error message: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] List<TModel> models)
        {
            try
            {
                await _CRUD.Add(models);
                return Ok("Added");
            }
            catch (Exception ex)
            {
                return BadRequest("Error message: " + ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] List<TModel> models)
        {
            try
            {
                await _CRUD.Update(models);
                return Ok("Updated");
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
