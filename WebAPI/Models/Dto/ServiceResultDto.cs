namespace Setia.Models
{
    public class LoginResultDto<T>
    {
        public T? Result { get; set; }
        public string? Message { get; set; } = null;
    }
}