using Main.Modules.Auth;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Standards.Data.Models;

public class BaseModel
{
    [Column(TypeName = "timestamptz")]
    public DateTime ExecutionDate { get; set; } = DateTime.Now.ToUniversalTime();
    public Guid? AuthorId { get; set; } = null;
    public UserModel? AuthorData { get; set; } = null;
    public List<string> Tags { get; set; } = new();
}