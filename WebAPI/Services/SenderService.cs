using Setia.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Setia.Services
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
            }
            return Task.CompletedTask;
        }

        public Task UploadFile(IFormFile file, string description) => throw new Exception();

    }
}