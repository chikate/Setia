using AutoMapper;
using Setia.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models
{
    [AutoMap(typeof(UserModel), ReverseMap = true)]
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