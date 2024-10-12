using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Data.Models;

public class NotificationModel : BaseModel
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [ForeignKey("User")]
    public Guid? ToUserId { get; set; }
    public AuditModel? ToUserData { get; set; }

    public string? Icon { get; set; } = "pi pi-info";
    public string? Severity { get; set; } = "info";
    public string Title { get; set; } = "Notification";
    public string? SubTitle { get; set; } = null;
    public string? Message { get; set; } = "You have been notified :)";
    public Dictionary<string, string>? ExtraMessages { get; set; } = null;

}