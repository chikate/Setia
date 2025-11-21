namespace Data.Models;

public class ParameterModel : BaseModel<Guid>
{
    public required string Name { get; set; }
    public string? Description { get; set; } = null;
}