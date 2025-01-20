using MailKit.Net.Smtp;
using MimeKit;

public static class Helper
{
    public static async Task SendEmailAsync(MimeMessage emailData)
    {
        IConfigurationSection emailSettings = new ConfigurationManager()
            .AddUserSecrets<Program>()
            .Build()
            .GetSection("SmtpSettings");

        using SmtpClient smtp = new();
        await smtp.ConnectAsync(emailSettings["Server"], int.Parse(emailSettings["Port"]!), bool.Parse(emailSettings["EnableSsl"]!));
        await smtp.AuthenticateAsync(emailSettings["Username"], emailSettings["Password"]);
        await smtp.SendAsync(emailData);
        await smtp.DisconnectAsync(true);
    }
}