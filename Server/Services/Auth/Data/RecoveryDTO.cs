using System.ComponentModel.DataAnnotations;

namespace Modules.Auth;

public class RecoveryDTO
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public required string Email { get; set; }
}