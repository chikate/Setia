using System.ComponentModel.DataAnnotations;

namespace Main.Data.Models;

public class UserModel : BaseModel
{
    #region Private Data
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Username { get; set; } = Guid.NewGuid().ToString("d").Substring(1, 8);
    public string Password { get; set; } = Guid.NewGuid().ToString("d").Substring(1, 8);
    public string Email { get; set; } = string.Empty;
    public string Signiture { get; set; } = string.Empty;
    #endregion

    #region Sensitive Data
    public DateTime? BirthDay { get; set; } = null;
    public List<Guid>? Friends { get; set; } = null;
    public Dictionary<string, string>? Saves { get; set; } = null;
    #endregion

    #region Public Data
    public string Name { get; set; } = string.Empty;
    public DateTime? EmailVerifiedDate { get; set; } = null;
    public string? Avatar { get; set; } = null;
    #endregion
}