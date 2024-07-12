namespace Setia.Models.Structs
{
    public class BaseAudit
    {
        public DateTime ExecutionDate { get; set; } = DateTime.SpecifyKind((DateTime)DateTime.Now!, DateTimeKind.Utc);
        public Guid? AuthorId { get; set; } = null;
    }
}