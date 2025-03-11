using Main.Modules.Gov.Models;
using System.ComponentModel.DataAnnotations;

namespace Main.Modules.Chat
{
    public class MessageModel : BaseModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid To { get; set; }
        public required string Message { get; set; }
        public DateTimeOffset? SentDate { get; set; }
        public required List<Guid>? Attachments { get; set; } = null;
    }
}