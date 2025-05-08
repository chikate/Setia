using Main.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Main.Modules.Drive;

/// <summary>
/// Model for file metadata.
/// </summary>
public class FileModel : BaseModel
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Description { get; set; } = null;
    public string? Path { get; set; } = null;
}