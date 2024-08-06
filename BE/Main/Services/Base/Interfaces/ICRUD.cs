namespace Main.Services.Base.Interfaces
{
    public interface ICRUD<TModel>
    {
        Task<IEnumerable<TModel>> Get(TModel? filter = default);
        Task<TModel> Add(TModel model);
        Task<TModel> Update(TModel model);
        Task Delete(string id);
    }
}