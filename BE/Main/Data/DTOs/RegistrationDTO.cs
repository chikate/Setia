namespace Main.Data.DTOs;

public class RegistrationDTO
{
    public string Username { get; set; } = Guid.NewGuid().ToString("d").Substring(1, 8);
    public string Password { get; set; } = Guid.NewGuid().ToString("d").Substring(1, 8);
    public string Email { get; set; } = string.Empty;
    public string Signiture { get; set; } = string.Empty;
    public DateTime? BirthDay { get; set; } = null;
    public string Name { get; set; } = string.Empty;
    public string? Avatar { get; set; } = null;
}