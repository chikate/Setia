using Setia.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setia.Models.Structs
{
    public class BaseAuditStruct
    {
        public DateTime ExecutionDate { get; set; } = DateTime.SpecifyKind((DateTime)DateTime.Now!, DateTimeKind.Utc);

        [ForeignKey("Author")]
        public string? AuthorId { get; set; }
        public UserModel? AuthorData { get; set; }
    }
}