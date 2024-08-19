using Main.Services.Base.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Main.Services.Base
{
    public class SenderService : ISender
    {
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

        public Task SendMail(string to, string subject, string message)
        {
            try
            {
                new SmtpClient("", 3)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(_config["Email:User"], _config["Email:Password"])
                }.SendMailAsync(new MailMessage(
                   from: _config["Email:User"] ?? "",
                   to,
                   subject,
                   message
                 ));
                return Task.CompletedTask;
            }
            catch (Exception ex) { _logger.LogError(ex.Message, GetType().FullName); throw; }
        }
        public Task UploadFile(IFormFile file, string description) => throw new();
    }
}