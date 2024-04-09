using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models
{
    public class AuditModel : BaseAuditStruct
    {
        [Key]
        public int Id { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public string Entity { get; set; } = string.Empty;
        public int? EntityId { get; set; }
        public string Payload { get; set; } = string.Empty;
    }
}