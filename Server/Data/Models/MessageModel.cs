using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

public class MessageModel : BaseModel<Guid>
{
    public Guid To { get; set; }
    public required string Message { get; set; }

    [Column(TypeName = "timestamptz")]
    public DateTimeOffset? SeenDate { get; set; } = null;

    public required List<Guid>? Attachments { get; set; } = null;
}