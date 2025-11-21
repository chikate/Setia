using Modules.Auth;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

public class BaseModel<TIDType>
{
    // [Key]
    public TIDType? Id { get; set; } //= Guid.NewGuid();

    [Column(TypeName = "timestamptz")]
    public DateTimeOffset? ExecutionDate { get; set; } = DateTime.Now.ToUniversalTime();
    public Guid? AuthorId { get; set; } = null;
    public UserModel? AuthorData { get; set; } = null;
    public List<string>? Tags { get; set; } = null;
}