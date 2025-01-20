using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Standards.Data.Models
{
    public class QuestionModel : BaseModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public required string Title { get; set; }
        public string? Comment { get; set; } = null;
        public List<string>? Options { get; set; } = null;
        public List<string>? Selection { get; set; } = null;
        public string? EndOption { get; set; } = null;
        [Column(TypeName = "timestamptz")]
        public DateTime? Expires { get; set; } = null;
    }
}