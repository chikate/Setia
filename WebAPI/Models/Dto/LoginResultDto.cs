using Setia.Models.Structs;

namespace Setia.Models
{
    public class LoginResultDto : BaseAuditStruct
    {
        public string? Token { get; set; }
        public UserModel? User { get; set; }
    }
}