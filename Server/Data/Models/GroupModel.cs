namespace Main.Data.Models
{
    public class GroupModel : BaseModel<Guid>
    {
        public required string Name { get; set; } = string.Empty;
        public string? Descriere { get; set; } = null;
    }
}