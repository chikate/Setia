using System.ComponentModel.DataAnnotations;

namespace Main.Data.Models
{
    public class SettingsModel : BaseModel
    {
        [Key]
        public Int64 Id { get; set; } = 0;

        public string Key { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Value { get; set; } = null;
    }
}