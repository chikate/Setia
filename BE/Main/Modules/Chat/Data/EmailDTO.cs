namespace Main.Modules.Chat.Data;

public class EmailDTO
{
    public required string To { get; set; }
    public string Subject { get; set; } = string.Empty;
    public required string Body { get; set; }
}