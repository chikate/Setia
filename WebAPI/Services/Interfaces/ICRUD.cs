namespace Setia.Services.Interfaces
{
    public interface ICRUD<TModel>
    {
        Task<List<TModel>> Add(List<TModel> models);
        Task<IEnumerable<TModel>> Get(List<TModel>? filters = default);
        Task<List<TModel>> Update(List<TModel> models);
        Task Delete(List<string> ids);
    }
}