using Main.Data.DTOs;
using System.Net;
using System.Net.Mail;

namespace Main.Services;

public interface ISenderService
{
    void SendMail(EmailDTO emailData);
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

    public void SendMail(EmailDTO emailData)
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
}