using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models.Base
{
    public class UserModel : DefinitionStruct
    {
        [Key]
        public required string Username { get; set; }

        public string Password { get; set; } = Guid.NewGuid().ToString("d").Substring(1, 8);
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime? EmailVerifiedDate { get; set; } = null;
    }
}