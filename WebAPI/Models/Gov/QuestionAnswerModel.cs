using Setia.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setia.Models.Gov
{
    public class QuestionAnswerModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<string>? Answer { get; set; }

        [ForeignKey("User")]
        public string? Username { get; set; }
        public UserModel? UserData { get; set; }

        [ForeignKey("Question")]
        public Guid QuestionId { get; set; }
        public QuestionModel? QuestionData { get; set; }
    }
}