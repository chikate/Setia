using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models.Gov
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
        public DateTime? Expires { get; set; } = null;
    }
}