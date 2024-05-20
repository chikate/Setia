namespace Setia.Services.Interfaces
{
    public interface ICRUD<TModel>
    {
        Task Add(TModel model);
        Task<IEnumerable<TModel>> Get(TModel? filter = default);
        Task Update(TModel model);
        Task Delete(string id);
    }
}