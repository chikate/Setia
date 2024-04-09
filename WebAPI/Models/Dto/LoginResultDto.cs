using Setia.Models.Structs;

namespace Setia.Models
{
    public class LoginResultDto : BaseAuditStruct
    {
        public string Token { get; set; } = string.Empty;
        public UserModel? User { get; set; }
    }
}