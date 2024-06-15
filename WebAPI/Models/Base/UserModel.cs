using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models.Base
{
    public class UserModel : BaseModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = Guid.NewGuid().ToString("d").Substring(1, 8);
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime? EmailVerifiedDate { get; set; } = null;
        public string? Avatar { get; set; } = null;
        public string? Signiture { get; set; } = null;
        public List<Guid>? Friends { get; set; } = null;
    }
}