﻿using Main.Standards.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Main.Modules.Audit;

public class AuditModel : BaseModel
{
    [Key]
    public long Id { get; set; } = 0;

    public string? Description { get; set; }
    public string? Details { get; set; }
    public string? EntityId { get; set; }
    public string? Entity { get; set; }
    public string? Payload { get; set; }
}