﻿using System.ComponentModel.DataAnnotations;

namespace Main.Modules.Gov.Models
{
    public class ServerModel : BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}