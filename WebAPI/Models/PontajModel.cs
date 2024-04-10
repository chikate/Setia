using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setia.Models
{
    public class PontajModel : DefinitionStruct
    {
        [Key]
        public int Id { get; set; } = 0;

        [ForeignKey("User")]
        public string? UserId { get; set; }
        public UserModel? User { get; set; }

        public DateTime BeginTime { get; set; } = DateTime.Now;
        public DateTime? EndTime { get; set; } = DateTime.Now.AddHours(8);
        public string Description { get; set; } = string.Empty;
    }
}