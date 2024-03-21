namespace Setia.Services.Interfaces
{
    public interface ICRUDOnlyGet<T>
    {
        IEnumerable<T> GetAll(T? filter);
    }
}