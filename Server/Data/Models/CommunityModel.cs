namespace Main.Data.Models;

public class CommunityModel : BaseModel<Guid>
{
    public string? Description { get; set; }
    public List<Guid>? Members { get; set; }
}