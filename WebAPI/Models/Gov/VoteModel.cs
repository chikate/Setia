using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models.Gov
{
    public class VoteModel : DefinitionStruct
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Title { get; set; }
        public required string Options { get; set; }
        public string? EndOption { get; set; }
        public DateTime? Available { get; set; }
    }
}