using Data.Models;

namespace Modules.Audit;

public class AuditModel : BaseModel<long>
{
    public string? Action { get; set; }
    public string? Details { get; set; }
    public string? EntityId { get; set; }
    public string? Entity { get; set; }
    public string? Payload { get; set; }
}