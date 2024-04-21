using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Setia.Models.Base;
using Setia.Services.Interfaces;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Setia.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class CRUDController<TModel> : ControllerBase
    {
        private readonly ICRUD<TModel> _CRUD;
        public CRUDController(ICRUD<TModel> CRUD) { _CRUD = CRUD; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TModel>>> Get(TModel? filter = default, string? user = null, string? specific = null)
        {
            try
            {
                IEnumerable<TModel> result = await _CRUD.Get(filter, user, specific);
                if (typeof(TModel) == typeof(TagModel))
                {
                    JsonSerializerOptions lTreeDataConverter = new() { WriteIndented = true };
                    lTreeDataConverter.Converters.Add(new LTreeDataConverter());
                    return Ok(JsonSerializer.Serialize(result, lTreeDataConverter));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error message: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TModel model)
        {
            try
            {
                await _CRUD.Add(model);
                return Ok("Added");
            }
            catch (Exception ex)
            {
                return BadRequest("Error message: " + ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TModel model)
        {
            try
            {
                await _CRUD.Update(model);
                return Ok("Updated");
            }
            catch (Exception ex)
            {
                return BadRequest("Error message: " + ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _CRUD.Delete(id);
                return Ok("Permanently deleted");
            }
            catch (Exception ex)
            {
                return BadRequest("Error message: " + ex.Message);
            }
        }
    }
}