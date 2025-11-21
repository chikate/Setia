namespace Data.Models;

public class LinkModel : BaseModel<Guid>
{
    public string SourceType { get; set; } = string.Empty; // e.g., "User"
    public Guid SourceId { get; set; }

    public string TargetType { get; set; } = string.Empty; // e.g., "UserRole"
    public Guid TargetId { get; set; }

    public string? Metadata { get; set; } = null;
}