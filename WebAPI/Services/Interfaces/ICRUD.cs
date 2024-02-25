namespace Setia.Services.Interfaces
{
    public interface ICRUD<T> : ICRUDOnlyGet<T>
    {
        Task<bool> Add(T model);
        Task<bool> Update(T model);
        Task<bool> Delete(int id);
    }
}