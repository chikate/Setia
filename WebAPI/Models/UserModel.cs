using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models
{
    public class UserModel : DefinitionStruct
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}