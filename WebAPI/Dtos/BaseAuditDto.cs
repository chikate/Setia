﻿using Setia.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setia.Structs
{
    public class BaseAuditDto
    {
        public DateTime ExecutionDate { get; set; } = DateTime.Now;

        [ForeignKey("Executioner")]
        public int? Id_Executioner { get; set; }
        public UserModel? Executioner { get; set; }
    }
}