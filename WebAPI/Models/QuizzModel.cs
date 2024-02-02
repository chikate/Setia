using AutoMapper;
using Setia.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models
{
    [AutoMap(typeof(QuizzModel), ReverseMap = true)]
    public class QuizzModel : DefinitionStruct
    {
        [Key]
        public int Id { get; set; } = 0;
        public string Question { get; set; } = string.Empty;
        public string Answere { get; set; } = string.Empty;
        public List<string>? Options { get; set; }
    }
}