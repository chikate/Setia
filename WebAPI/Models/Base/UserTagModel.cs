using Microsoft.EntityFrameworkCore;
using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setia.Models.Base
{
    public class UserTagModel : DefinitionStruct
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("Tag")]
        public required LTree TagId { get; set; }
        public TagModel? TagData { get; set; }

        [ForeignKey("User")]
        public string? Username { get; set; }
        public UserModel? UserData { get; set; }
    }
}