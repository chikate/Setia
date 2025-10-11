using Main.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Main.Features.CRUDs;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class CRUDController<TModel>(ICRUDService<TModel> CRUD) : ControllerBase where TModel : BaseModel<Guid>
{
    [HttpGet]
    public Task<List<TModel>> Get([FromQuery] GetFilterDTO<TModel> filter)
        => CRUD.Get(filter);

    [HttpPost]
    public Task<List<TModel>> Add([FromBody] List<TModel> models)
        => CRUD.Add(models);

    [HttpPut]
    public Task<int> Update([FromBody] List<TModel> models)
        => CRUD.Update(models);

    [HttpDelete]
    public Task<List<TModel>> Delete(List<Guid> ids)
        => CRUD.Delete(ids);
}
