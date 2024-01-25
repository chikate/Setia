using System.ComponentModel.DataAnnotations;
using Setia.Structs;

namespace Setia.Models
{
    public class UserModel : DefinitionStruct
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? StatusCode { get; set; } = null;
        public int? AuthorityCode { get; set; } = null;
    }
}