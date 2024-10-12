using Main.Data.DTOs;
using Main.Data.Models;

namespace Main.Services.Interfaces;

public interface IAuth
{
    Task<object?> Login(AuthenticationDTO loginCredentials);
    Task<UserModel> Register(RegistrationDTO registration);
    Task<UserModel?> GetCurrentUser();
    Task<IEnumerable<string>> GetUserTags(string? specific = ".", Guid? userId = null);
    Task<string> CriptPassword(string password);
    Task<List<string>> CheckUserRights(IEnumerable<string> rightsToCeck, Guid? user = null);
}
