using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models
{
    public class UserTagModel : DefinitionStruct
    {
        [Key]
        public int Id { get; set; } = 0;
        public required Guid Tag { get; set; }
        public required string User { get; set; }
    }
}