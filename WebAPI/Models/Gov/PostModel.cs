using Setia.Models.Base;
using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setia.Models.Gov
{
    public class PostModel : DefinitionStruct
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public required string User { get; set; }
        public UserModel? UserData { get; set; }

        public string? Comment { get; set; } = null;

        [ForeignKey("Question")]
        public Guid? Question { get; set; } = null;
        public QuestionModel? QuestionData { get; set; } = null;

        public string? Description { get; set; } = null;
    }
}