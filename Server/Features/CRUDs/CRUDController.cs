using Main.Modules.Auth;
using Main.Modules.Gov.Models;
using Microsoft.AspNetCore.Mvc;

namespace Main.Features.CRUDs;

[Route("api/[controller]/[action]")]
public abstract class CRUDController<TModel>(ICRUDService<TModel> CRUD, IAuthService auth) : ControllerBase where TModel : BaseModel
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TModel>>> Get([FromQuery] GetFilterDTO<TModel> filter)
    {
        try
        {
            await auth.CheckUserRight(filePath: typeof(TModel).Name);
            return Ok(await CRUD.Get(filter));
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] TModel model)
    {
        try
        {
            await auth.CheckUserRight(filePath: typeof(TModel).Name);
            return Created(Url.Action(nameof(Add)), await CRUD.Add(model));
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TModel model)
    {
        try
        {
            await auth.CheckUserRight(filePath: typeof(TModel).Name);
            return Ok(await CRUD.Update(model));
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid guid)
    {
        try
        {
            await auth.CheckUserRight(filePath: typeof(TModel).Name);
            return await CRUD.Delete(guid) ? Ok($"{guid} Deleted") : NotFound("Entity Not Found, Guid Invalid");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}
