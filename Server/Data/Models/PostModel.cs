using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Data.Models
{
    public class PostModel : BaseModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Message { get; set; } = null;

        #region Embeds
        [ForeignKey("FK_Post_Question")]
        public Guid? QuestionId { get; set; } = null;
        public QuestionModel? QuestionData { get; set; } = null;
        #endregion
    }
}