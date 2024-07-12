using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models.Base
{
    public class RoleModel : BaseModel
    {
        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; }
        public string? Color { get; set; }
        public string? Icon { get; set; }
        public List<string>? Rights { get; set; }
    }
}