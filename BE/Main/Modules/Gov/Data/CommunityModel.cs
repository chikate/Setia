using System.ComponentModel.DataAnnotations;

namespace Main.Modules.Gov.Models;

public class CommunityModel : BaseModel
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Description { get; set; }
    public IList<string>? Members { get; set; }
}