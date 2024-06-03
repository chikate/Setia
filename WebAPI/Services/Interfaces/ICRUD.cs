namespace Setia.Services.Interfaces
{
    public interface ICRUD<TModel>
    {
        Task Add(List<TModel> models);
        Task<IEnumerable<TModel>> Get(List<TModel>? filters = default);
        Task Update(List<TModel> models);
        Task Delete(List<string> ids);
    }
}