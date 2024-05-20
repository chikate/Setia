using Setia.Models.Base;

namespace Setia.Services.Interfaces
{
    public interface IAuth
    {
        UserModel GetCurrentUser();
        Task<IEnumerable<string>> GetUserTags(string? username = null, string? specific = null);
        Task Register(UserModel registration);
        string CriptPassword(string password);
    }
}
