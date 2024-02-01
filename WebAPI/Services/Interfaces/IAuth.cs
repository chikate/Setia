namespace Setia.Services.Interfaces
{
    public interface IAuth
    {
        Task<int> GetCurrentUser();
    }
}