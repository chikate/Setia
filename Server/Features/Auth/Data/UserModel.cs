using Main.Modules.Gov.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Modules.Auth;

public class UserModel : BaseModel
{
    #region Private Data
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Username { get; set; } = Guid.NewGuid().ToString("d").Substring(1, 8);
    public string Password { get; set; } = Guid.NewGuid().ToString("d").Substring(1, 8);
    public string Email { get; set; } = string.Empty;
    public string? Signiture { get; set; } = null;
    #endregion

    #region Sensitive Data
    [Column(TypeName = "timestamptz")]
    public DateTimeOffset? BirthDay { get; set; } = null;
    public List<Guid> Friends { get; set; } = new();
    public List<Guid> Saves { get; set; } = new();
    #endregion

    #region Public Data
    public string Name { get; set; } = string.Empty;
    [Column(TypeName = "timestamptz")]
    public DateTimeOffset? EmailVerifiedDate { get; set; } = null;
    public string? Avatar { get; set; } = null;
    #endregion
}