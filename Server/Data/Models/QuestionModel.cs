namespace Main.Data.Models
{
    public class QuestionModel : BaseModel<Guid>
    {
        public required string Title { get; set; }
        public List<string>? Comments { get; set; } = null;
        public List<string>? Options { get; set; } = null;
        public List<string>? Selection { get; set; } = null;
        public List<string>? CorrectAnswers { get; set; } = null;
    }
}