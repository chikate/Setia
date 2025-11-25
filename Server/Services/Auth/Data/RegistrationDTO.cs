using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modules.Auth;

public class RegistrationDTO
{
    [Required]
    [MinLength(6, ErrorMessage = "Username must be at least 6 characters long")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string Password { get; set; } = Guid.NewGuid().ToString("d");

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public required string Email { get; set; } = string.Empty;

    public string Signiture { get; set; } = string.Empty;
    
    [Column(TypeName = "timestamptz")]
    public DateTimeOffset? BirthDay { get; set; } = null;
    
    public string Name { get; set; } = string.Empty;
    public string? Avatar { get; set; } = null;
}