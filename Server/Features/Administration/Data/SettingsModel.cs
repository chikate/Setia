using Main.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Main.Modules.Adm;

public class SettingsModel : BaseModel
{
    [Key]
    public long Id { get; set; } = 0;

    public string Key { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Value { get; set; } = null;
}