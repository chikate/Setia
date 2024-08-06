using Main.Data.Models.Base;

namespace Main.Services.Base.Interfaces
{
    public interface IAuth
    {
        Task<object?> Login(UserModel loginCredentials);
        Task<bool> Register(UserModel registration);
        Task<UserModel?> GetCurrentUser();
        Task<IEnumerable<string>> GetUserTags(string? specific = ".", Guid? userId = null);
        Task<string> CriptPassword(string password);
        Task<List<string>> CheckUserRights(IEnumerable<string> rightsToCeck, Guid? user = null);
    }
}
