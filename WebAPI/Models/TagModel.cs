using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setia.Models
{
    public class TagModel : DefinitionStruct
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [ForeignKey("Parent")]
        public Guid? ParentGuid { get; set; }
        public TagModel? Parent { get; set; }
    }
}