using Setia.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Setia.Services
{
    public class SenderService : ISender
    {
        private readonly ILogger<SenderService> _logger;

        public SenderService
        (
            ILogger<SenderService> logger
        )
        {
            _logger = logger;
        }

        public Task Send(string to, string subject, string message)
        {
            try
            {
                string mail = "";
                new SmtpClient("", 3)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(mail, "")
                }.SendMailAsync(new MailMessage(
                   from: mail,
                   to,
                   subject,
                   message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
            }
            return Task.CompletedTask;
        }

        public Task UploadFile(IFormFile file, string description)
        {
            throw new NotImplementedException();
        }
    }
}