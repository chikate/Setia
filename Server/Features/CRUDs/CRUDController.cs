using Main.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Main.Features.CRUDs;

[Route("api/[controller]/[action]")]
public abstract class CRUDController<TModel>(ICRUDService<TModel> CRUD) : ControllerBase where TModel : BaseModel
{
    [HttpGet]
    public Task<IEnumerable<TModel>> Get([FromQuery] GetFilterDTO<TModel> filter)
        => CRUD.Get(filter);

    [HttpPost]
    public Task<IEnumerable<TModel>> Add([FromBody] List<TModel> models)
        => CRUD.Add(models);

    [HttpPut]
    public Task<IEnumerable<TModel>> Update([FromBody] List<TModel> models)
        => CRUD.Update(models);

    [HttpDelete]
    public Task<IEnumerable<TModel>> Delete(List<Guid> ids)
        => CRUD.Delete(ids);
}
