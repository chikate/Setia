namespace Data.Models;

public class GroupModel : BaseModel<Guid>
{
    public required string Name { get; set; }
    public string? Description { get; set; } = null;
}