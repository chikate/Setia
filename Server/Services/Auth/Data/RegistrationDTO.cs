using System.ComponentModel.DataAnnotations.Schema;

namespace Modules.Auth;

public class RegistrationDTO
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = Guid.NewGuid().ToString("d");
    public required string Email { get; set; } = string.Empty;
    public string Signiture { get; set; } = string.Empty;
    [Column(TypeName = "timestamptz")]
    public DateTimeOffset? BirthDay { get; set; } = null;
    public string Name { get; set; } = string.Empty;
    public string? Avatar { get; set; } = null;
}