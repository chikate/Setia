namespace Main.Data.Models
{
    public class PostModel : BaseModel<Guid>
    {
        public Guid? To { get; set; } = null;
        public required string Message { get; set; }
        public DateTimeOffset? SentDate { get; set; }
        public required List<Guid>? Attachments { get; set; } = null;
    }
}