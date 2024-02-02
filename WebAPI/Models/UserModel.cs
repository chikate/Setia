using AutoMapper;
using Setia.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models
{
    [AutoMap(typeof(UserModel), ReverseMap = true)]
    public class UserModel : DefinitionStruct
    {
        [Key]
        public int Id { get; set; } = 0;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int? StatusCode { get; set; } = null;
        public int? AuthorityCode { get; set; } = null;
    }
}