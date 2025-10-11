using Main.Data.Models;

namespace Main.Modules.Drive;

public class FileModel : BaseModel<Guid>
{
    public string? Description { get; set; } = null;
    public string? Path { get; set; } = null;
}