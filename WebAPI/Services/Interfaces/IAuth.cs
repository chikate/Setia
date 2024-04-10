using Setia.Models;

namespace Setia.Services.Interfaces
{
    public interface IAuth
    {
        UserModel GetCurrentUser();
        Task<IEnumerable<string>> GetUserRights(string username);
        Task<IEnumerable<string>> GetUserRoles(string username);
        Task Register(UserModel registration);
        Task GiveUserTag(Guid tag, string username);
        Task RemoveUserTag(Guid tag, string username);
        Task GetUserTags(Guid tag, string username);
    }
}
