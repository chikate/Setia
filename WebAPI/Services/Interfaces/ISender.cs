namespace Setia.Services.Interfaces
{
    public interface ISender
    {
        Task SendEmail(string email, string subject, string message);
    }
}