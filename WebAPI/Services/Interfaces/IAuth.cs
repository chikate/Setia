using Setia.Models;

namespace Setia.Services.Interfaces
{
    public interface IAuth
    {
        Task<int> GetCurrentUserId();
        Task<IEnumerable<string>> GetUserRights(int id_user);
        Task<IEnumerable<string>> GetUserRoles(int id_user);
        IEnumerable<object> GetActions();
        Task Register(UserModel registration);
        Task AssignRoleToUser(int id_claim, int id_user);
        Task RemoveRoleFromUser(int id_claim, int id_user);
        Task AssignClaimToUser(int id_claim, int id_user);
        Task RemoveClaimFromUser(int id_claim, int id_user);
    }
}
