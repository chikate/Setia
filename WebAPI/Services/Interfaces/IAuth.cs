using Setia.Models.Base;

namespace Setia.Services.Interfaces
{
    public interface IAuth
    {
        Task<object> Login(UserModel loginCredentials);
        Task Register(UserModel registration);
        UserModel? GetCurrentUser();
        Task<IEnumerable<string>> GetUserTags(string? specific = ".", Guid? userId = null);
        string CriptPassword(string password);
        Task<List<string>> CheckUserRights(IEnumerable<string> rightsToCeck, Guid? user = null);
    }
}
