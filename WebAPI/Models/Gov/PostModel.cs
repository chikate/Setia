using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setia.Models.Gov
{
    public class PostModel : BaseModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Message { get; set; } = null;

        [ForeignKey("FK_Post_Question")]
        public Guid? QuestionId { get; set; } = null;
        public QuestionModel? QuestionData { get; set; } = null;

        [ForeignKey("FK_Post_ToPost")]
        public Guid? ToPostId { get; set; } = null;
        public PostModel? ToPostData { get; set; } = null;
    }
}