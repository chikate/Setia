using Setia.Structs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setia.Models
{
    public class RoleModel : DefinitionStruct
    {
        [Key]
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public List<string>? Rights { get; set; }

        [ForeignKey("InheritedRole")]
        public int? InheritsRole_Id { get; set; }
        public RoleModel? InheritsRole { get; set; }
    }
}