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
        public required LTree Tag { get; set; }
        public TagModel? TagData { get; set; }

        [ForeignKey("User")]
        public required string User { get; set; }
        public UserModel? UserData { get; set; }
    }
}