using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setia.Models.Gov
{
    public class QuestionAnswerModel : DefinitionStruct
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public required List<string> Answer { get; set; }

        [ForeignKey("Question")]
        public required Guid Question { get; set; }
        public QuestionModel? QuestionData { get; set; }
    }
}