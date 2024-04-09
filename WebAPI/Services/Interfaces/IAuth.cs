using Setia.Models;

namespace Setia.Services.Interfaces
{
    public interface IAuth
    {
        Task<UserModel> GetCurrentUser();
        Task<IEnumerable<string>> GetUserRights(int idUser);
        Task<IEnumerable<string>> GetUserRoles(int idUser);
        Task Register(UserModel registration);
        Task AssignRoleToUser(int idClaim, int idUser);
        Task RemoveRoleFromUser(int idClaim, int idUser);
        Task AssignClaimToUser(int idClaim, int idUser);
        Task RemoveClaimFromUser(int idClaim, int idUser);
    }
}
