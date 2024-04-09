using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setia.Models
{
    public class TagModel : DefinitionStruct
    {
        [Key]
        public Guid Guid { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;

        [ForeignKey("ParentTag")]
        public Guid? ParentTagGuid { get; set; } = null;
        public TagModel? ParentTag { get; set; }
    }
}