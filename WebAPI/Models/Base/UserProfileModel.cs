using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setia.Models.Base
{
    public class UserProfileModel : DefinitionStruct
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public required string User { get; set; }
        public UserModel? UserData { get; set; }

        public string? Avatar { get; set; } = null;
        public string? Signiture { get; set; } = null;
        public string? Name { get; set; } = null;
    }
}