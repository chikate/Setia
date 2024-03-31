namespace Setia.Services.Interfaces
{
    public interface ICRUDOnlyGet<T>
    {
        IEnumerable<T> Get(T? filter);
    }
}