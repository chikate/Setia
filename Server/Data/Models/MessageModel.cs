using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

public class MessageModel : BaseModel<Guid>
{
    public Guid? To { get; set; } // Nullable for Group Messages
    public Guid? GroupId { get; set; } // Nullable for DMs

    public Guid? ReplyToId { get; set; } // ID of the message being replied to

    public required string Message { get; set; }

    [Column(TypeName = "timestamptz")]
    public DateTimeOffset? SeenDate { get; set; } = null;

    public required List<Guid>? Attachments { get; set; } = null;
    
    // JSON string for reactions: { "userId": ["emoji1", "emoji2"] }
    public string? Reactions { get; set; } = null;

    public ICollection<MessageHistoryModel> History { get; set; } = new List<MessageHistoryModel>();

    [NotMapped]
    public bool IsEdited => History.Any();
}