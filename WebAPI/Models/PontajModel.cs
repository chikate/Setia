using AutoMapper;
using Setia.Structs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setia.Models
{
    [AutoMap(typeof(PontajModel), ReverseMap = true)]
    public class PontajModel : DefinitionStruct
    {
        [Key]
        public int Id { get; set; } = 0;

        [ForeignKey("User")]
        public int Id_User { get; set; }
        public UserModel? User { get; set; }

        public DateTime BeginTime { get; set; } = DateTime.Now;
        public DateTime? EndTime { get; set; } = DateTime.Now.AddHours(8);
        public string? Description { get; set; } = string.Empty;
    }
}