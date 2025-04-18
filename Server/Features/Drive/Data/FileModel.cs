using Main.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Main.Modules.Drive;

public class FileModel : BaseModel
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Description { get; set; } = null;
    public string? Path { get; set; } = null;
}