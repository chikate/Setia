using Setia.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models
{
    public class AuditModel : BaseAuditStruct
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
    }
}