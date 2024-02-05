using Setia.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models
{
    public class QuizzModel : DefinitionDto
    {
        [Key]
        public int Id { get; set; } = 0;
        public string Question { get; set; } = string.Empty;
        public string Answere { get; set; } = string.Empty;
        public List<string>? Options { get; set; }
    }
}