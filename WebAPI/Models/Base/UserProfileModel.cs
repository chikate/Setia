using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models.Base
{
    public class UserProfileModel : DefinitionStruct
    {
        [Key]
        public string Username { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public string Signiture { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}