#pragma warning disable CS8604 // Possible null reference argument.

using Main.Data.DTOs;
using System.Net;
using System.Net.Mail;

namespace Main.Services;

public interface ISenderService
{
    Task SendMail(EmailDTO emailData);
}

public class SenderService : ISenderService
{
    #region Dependency Injection 
    private readonly ILogger<SenderService> _logger;
    private readonly IConfiguration _config;

    public SenderService
    (
        ILogger<SenderService> logger,
        IConfiguration config
    )
    {
        _logger = logger;
        _config = config;
    }
    #endregion

    public async Task SendMail(EmailDTO emailData)
    {
        try
        {
            using (var smtpClient = new SmtpClient(_config["Email:SmtpServer"], int.Parse(_config["Email:Port"])))
            {
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(_config["Email:User"], _config["Email:Password"]);
                await smtpClient.SendMailAsync(new MailMessage
                {
                    From = new MailAddress(_config["Email:User"]),
                    //To = emailData.To,
                    Subject = emailData.Subject,
                    Body = emailData.Body,
                    IsBodyHtml = true // Set to true if your email body is HTML
                });
            }
        }
        catch (Exception ex) { _logger.LogError(ex, GetType().FullName, ex.Message); throw; }
    }
}