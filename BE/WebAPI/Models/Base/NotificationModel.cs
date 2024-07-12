using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setia.Models.Base
{
    public class NotificationModel : BaseModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid? UserId { get; set; }
        public UserModel? UserData { get; set; }

        public string? Icon { get; set; }
        public string? Title { get; set; }
        public string? Comment { get; set; }
    }
}