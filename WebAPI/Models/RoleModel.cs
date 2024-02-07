using Setia.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models
{
    public class RoleModel : DefinitionDto
    {
        [Key]
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
    }
}