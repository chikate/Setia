using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

public class MessageHistoryModel : BaseModel<Guid>
{
    public Guid MessageId { get; set; }
    
    [ForeignKey("MessageId")]
    public MessageModel? Message { get; set; }

    public required string PreviousContent { get; set; }
    
    [Column(TypeName = "timestamptz")]
    public DateTimeOffset EditedDate { get; set; }
}
