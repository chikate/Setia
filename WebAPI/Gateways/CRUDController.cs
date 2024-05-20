using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Setia.Services.Interfaces;
using System.Reflection;
using System.Text.Json;

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
        public async Task<ActionResult<IEnumerable<TModel>>> Get(/*[FromQuery] TModel? filter = default*/)
        {
            try
            {
                return Ok(JsonSerializer.Serialize(await _CRUD.Get(default),
                    new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        Converters = { new LTreeConverter() }
                    }));
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
                //PropertyInfo? list = model?.GetType().GetProperties().Where(p => p.Name.Contains("List")).FirstOrDefault();
                //PropertyInfo? listData = model?.GetType().GetProperties().Where(p => p.Name.Contains(list?.Name.Replace("List", ""))).FirstOrDefault();
                //listData?.SetValue(model, string.Join(",", (List<string>)list?.GetValue(model)));

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
        public async Task<IActionResult> Delete(string id)
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
