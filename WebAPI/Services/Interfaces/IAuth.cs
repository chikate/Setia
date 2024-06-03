using Setia.Models.Base;
using System.Collections;

namespace Setia.Services.Interfaces
{
    public interface IAuth
    {
        UserModel? GetCurrentUser();
        Task<IEnumerable<string>> GetUserTags(string specific, string ? username = null);
        Task Register(UserModel registration);
        string CriptPassword(string password);
        Task<List<string>> CheckUserRights(IEnumerable<string> rightsToCeck, string? user = null);
    }
}
