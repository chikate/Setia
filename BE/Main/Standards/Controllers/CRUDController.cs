using Main.Modules.Auth;
using Main.Standards.Data.Models;
using Main.Standards.Services;
using Microsoft.AspNetCore.Mvc;

namespace Main.Standards.CRUDController;

public abstract class CRUDController<TModel> : StandardAPI where TModel : BaseModel
{
    #region Dependency Injection
    private readonly ICRUDService<TModel> _CRUD;
    private readonly IAuthService _auth;
    public CRUDController
    (
        ICRUDService<TModel> CRUD,
        IAuthService auth
    )
    {
        _CRUD = CRUD;
        _auth = auth;
    }
    #endregion

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TModel>>> Get([FromQuery] GetFilterDTO<TModel> filter)
    {
        try
        {
            await _auth.CheckUserRight(filePath: typeof(TModel).Name);
            return Ok(await _CRUD.Get(filter));
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] TModel model)
    {
        try
        {
            await _auth.CheckUserRight(filePath: typeof(TModel).Name);
            return Created(Url.Action(nameof(Add)), await _CRUD.Add(model));
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TModel model)
    {
        try
        {
            await _auth.CheckUserRight(filePath: typeof(TModel).Name);
            return Ok(await _CRUD.Update(model));
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid guid)
    {
        try
        {
            await _auth.CheckUserRight(filePath: typeof(TModel).Name);
            return await _CRUD.Delete(guid) ? Ok($"{guid} Deleted") : NotFound("Entity Not Found, Guid Invalid");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}
