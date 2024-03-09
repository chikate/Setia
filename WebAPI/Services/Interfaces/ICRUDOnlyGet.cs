namespace Setia.Services.Interfaces
{
    public interface ICRUDOnlyGet<T>
    {
        Task<IEnumerable<T>> GetAll(T? filter);
    }
}