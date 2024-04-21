namespace Setia.Services.Interfaces
{
    public interface ICRUD<TModel>
    {
        Task Add(TModel model);
        Task<IEnumerable<TModel>> Get(TModel? filter = default, string? user = null, string? specific = null);
        Task Update(TModel model);
        Task Delete(int id);
    }
}