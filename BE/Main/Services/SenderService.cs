using Main.Data.DTOs;
using Main.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Main.Services;

public class SenderService : ISender
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
            new SmtpClient("", 3)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_config["Email:User"], _config["Email:Password"])
            }
            ?.SendMailAsync(new MailMessage(
               from: "",
               to: emailData.To,
               subject: emailData.Subject,
               body: emailData.Body
             ));
        }
        catch (Exception ex) { _logger.LogError(ex.Message, GetType().FullName); throw; }
    }
    public Task UploadFile(IFormFile file, string description) => throw new();
}