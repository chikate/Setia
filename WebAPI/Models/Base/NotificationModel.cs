using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setia.Models.Base
{
    public class NotificationModel : DefinitionStruct
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public required string User { get; set; }
        public UserModel? UserData { get; set; }

        public string? Icon { get; set; }
        public string? Title { get; set; }
        public string? Comment { get; set; }
    }
}