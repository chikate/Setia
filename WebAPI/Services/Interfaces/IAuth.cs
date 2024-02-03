using Setia.Models;

namespace Setia.Services.Interfaces
{
    public interface IAuth
    {
        UserModel GetCurrentUser();
    }
}
