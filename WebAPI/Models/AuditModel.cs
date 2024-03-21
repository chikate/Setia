﻿using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models
{
    public class AuditModel : BaseAuditStruct
    {
        [Key]
        public int Id { get; set; } = 0;
        public string? Description { get; set; } = null;
        public string? Details { get; set; } = null;
        public string? Entity { get; set; } = null;
        public int? Id_Entity { get; set; } = null;
        public string? Payload { get; set; } = null;
    }
}