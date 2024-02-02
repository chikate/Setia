using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models
{
    [AutoMap(typeof(AuditModel), ReverseMap = true)]
    public class AuditModel
    {
        [Key]
        public int Id { get; set; } = 0;
        public string? Description { get; set; } = null;
        public string? Details { get; set; } = null;
        public string? Entity { get; set; } = null;
        public int? Id_Entity { get; set; } = null;
        public DateTime ExecutionDate { get; set; } = DateTime.Now;
        public int? Id_Executioner { get; set; } = 7;
        public string? Payload { get; set; } = null;
    }
}