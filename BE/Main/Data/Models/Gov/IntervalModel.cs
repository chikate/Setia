using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Data.Models
{
    public class IntervalModel : BaseModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid? UserId { get; set; } = null;
        public AuditModel? UserData { get; set; } = null;

        public DateTime BeginTime { get; set; } = DateTime.SpecifyKind(DateTime.Now!, DateTimeKind.Utc);
        public DateTime? EndTime { get; set; } = DateTime.SpecifyKind(DateTime.Now.AddDays(8)!, DateTimeKind.Utc);
        public string? Description { get; set; } = string.Empty;
    }
}