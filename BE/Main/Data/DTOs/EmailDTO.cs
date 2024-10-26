using System.Net.Mail;

namespace Main.Data.DTOs;

public class EmailDTO
{
    public required List<string> To { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}