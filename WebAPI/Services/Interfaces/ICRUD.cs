namespace Setia.Services.Interfaces
{
    public interface ICRUD<T>
    {
        Task<bool> Add(T model);        // C
        IEnumerable<T> Get(T? filter);  // R
        Task<bool> Update(T model);     // U
        Task<bool> Delete(int id);      // D
    }
}