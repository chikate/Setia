namespace Setia.Services.Interfaces
{
    public interface ISender
    {
        Task Send(string to, string subject, string message);
        Task UploadFile(IFormFile file, string description);
    }
}