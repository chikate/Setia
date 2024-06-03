namespace Setia.Models.Structs
{
    public class BaseAuditStruct
    {
        public DateTime ExecutionDate { get; set; } = DateTime.SpecifyKind((DateTime)DateTime.Now!, DateTimeKind.Utc);
        public string? Author { get; set; }
    }
}