using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models.Base
{
    public class ServerModel : BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public RoleModel? Roles { get; set; }
    }
}