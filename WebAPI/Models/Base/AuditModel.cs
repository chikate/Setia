using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models.Base
{
    public class AuditModel : BaseAudit
    {
        [Key]
        public int Id { get; set; } = 0;

        public string? Description { get; set; }
        public string? Details { get; set; }
        public string? Entity { get; set; }
        public string? EntityId { get; set; }
        public string? Payload { get; set; }
    }
}