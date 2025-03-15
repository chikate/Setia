using Main.Modules.Audit;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Modules.Gov.Models
{
    public class IntervalModel : BaseModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid? UserId { get; set; } = null;
        public AuditModel? UserData { get; set; } = null;

        [Column(TypeName = "timestamptz")]
        public DateTimeOffset BeginTime { get; set; } = DateTime.Now.ToUniversalTime();
        [Column(TypeName = "timestamptz")]
        public DateTimeOffset? EndTime { get; set; } = DateTime.Now.ToUniversalTime().AddDays(8);
        public string? Description { get; set; } = string.Empty;
    }
}