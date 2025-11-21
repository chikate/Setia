namespace Data.Models;

public class NotificationModel : MessageModel
{
    public required string Title { get; set; }
    public string? Link { get; set; }
}
