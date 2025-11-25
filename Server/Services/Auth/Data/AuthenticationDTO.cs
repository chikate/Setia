using System.ComponentModel.DataAnnotations;

namespace Modules.Auth;

public class AuthenticationDTO
{
    [Required]
    [MinLength(6, ErrorMessage = "Username must be at least 6 characters long")]
    public required string Username { get; set; }

    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public required string Password { get; set; }
}