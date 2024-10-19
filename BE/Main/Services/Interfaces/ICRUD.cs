using Main.Data.DTOs;

namespace Main.Services.Interfaces;

public interface ICRUD<TModel>
{
    Task<IEnumerable<TModel>> Get(GetFilterDTO<TModel> filter = null);
    Task<TModel> Add(TModel model);
    Task<TModel> Update(TModel model);
    Task Delete(string id);
}