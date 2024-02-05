using Setia.Structs;

namespace Setia.Services.Interfaces
{
    public interface IAuth
    {
        Task<int> GetCurrentUserId();
        Task Register(RegistrationDto registration);
        IEnumerable<string> GetAllRights();
    }
}
