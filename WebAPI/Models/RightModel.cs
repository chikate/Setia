using Setia.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models
{
    public class RightModel : DefinitionStruct
    {
        [Key]
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string? Action { get; set; }
        public string? Filter { get; set; }
    }
}