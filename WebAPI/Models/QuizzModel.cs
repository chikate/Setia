using AutoMapper;
using Setia.Structs;
using System.ComponentModel.DataAnnotations;

namespace Setia.Models
{
    [AutoMap(typeof(QuizzModel), ReverseMap = true)]
    public class QuizzModel : DefinitionStruct
    {
        [Key]
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answere { get; set; }
        public List<string>? Options { get; set; }
    }
}