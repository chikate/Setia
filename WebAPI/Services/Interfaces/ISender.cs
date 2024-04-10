namespace Setia.Services.Interfaces
{
    public interface ISender
    {
        Task SendMail(string to, string subject, string message);
        Task UploadFile(IFormFile file, string description);
    }
}