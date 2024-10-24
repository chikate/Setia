using System.ComponentModel.DataAnnotations;

namespace Main.Data.Models;

public class RegistryModel : BaseModel
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Entity { get; set; } = null;
    public string? EntityId { get; set; } = null;
    public int? RegistryNumber { get; set; } = null;
}