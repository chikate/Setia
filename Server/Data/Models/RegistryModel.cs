﻿using System.ComponentModel.DataAnnotations;

namespace Main.Data.Models;

public class RegistryModel : BaseModel
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Entity { get; set; }
    public required string EntityId { get; set; }
}