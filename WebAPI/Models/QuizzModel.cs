using System.ComponentModel.DataAnnotations;
using Setia.Structs;

namespace Setia.Models
{
    public class QuizzModel : DefinitionStruct
    {
        [Key]
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answere { get; set; }
        public string? Options { get; set; }
    }
}