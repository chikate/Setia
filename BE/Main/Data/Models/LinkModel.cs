using System.ComponentModel.DataAnnotations;

namespace Main.Data.Models;

public class LinkModel
{
    [Key]
    public Guid Id { get; set; }

    public string FromEntity { get; set; }
    public string ToEntity { get; set; }
    public string? Type { get; set; }
    public string? Description { get; set; }
    public List<string>? Tags { get; set; } = null;
}