namespace Main.Data.Models;

public class BaseModel
{
    public DateTime ExecutionDate { get; set; } = DateTime.Now.ToUniversalTime();
    public Guid? AuthorId { get; set; } = null;
    public AuditModel? AuthorData { get; set; } = null;
    public List<string> Tags { get; set; } = new();
}