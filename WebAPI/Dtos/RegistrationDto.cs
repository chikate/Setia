namespace Setia.Structs
{
    public class RegistrationDto
    {
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public LoginDto Login { get; set; } = new LoginDto();
    }
}