using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models.Gov
{
    public class QuestionModel : DefinitionStruct
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public required string Title { get; set; }
        public string? Comment { get; set; }
        public List<string>? Options { get; set; }
        public List<string>? Selection { get; set; }
        public string? EndOption { get; set; }
        public DateTime? Expires { get; set; }
    }
}