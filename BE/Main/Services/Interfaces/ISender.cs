using Main.Data.DTOs;

namespace Main.Services.Interfaces;

public interface ISender
{
    Task SendMail(EmailDTO emailData);
    Task UploadFile(IFormFile file, string description);
}