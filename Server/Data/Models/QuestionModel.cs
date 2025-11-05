namespace Main.Data.Models
{
    public class QuestionModel : BaseModel<Guid>
    {
        public required string Title { get; set; }
        public List<string>? Options { get; set; } = null;
        public int? Selection { get; set; } = null;
        public int? CorrectAnswers { get; set; } = null;
    }
}