using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

[Table("Tracks")]
public class TrackModel : BaseModel<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Artist { get; set; } = string.Empty;
    public string Album { get; set; } = string.Empty;
    public double Duration { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public string MimeType { get; set; } = "audio/mpeg";
}
