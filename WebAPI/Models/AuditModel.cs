using AutoMapper;
using Setia.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models
{
    [AutoMap(typeof(AuditModel),ReverseMap = true)]
    public class AuditModel : BaseAuditStruct
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        public string? Entity { get; set; }
        public int? Id_Entity { get; set; }
    }
}