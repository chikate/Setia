using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Data.Models
{
    public class NotificationModel : BaseModel<Guid>
    {
        public required string Message { get; set; }

        [Column(TypeName = "timestamptz")]
        public DateTimeOffset? ReadDate { get; set; } = null;

        public required List<Guid>? Attachments { get; set; } = null;
    }
}