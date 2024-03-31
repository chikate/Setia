using System.ComponentModel.DataAnnotations.Schema;

namespace Setia.Models.Structs
{
    public class BaseAuditStruct
    {
        public DateTime ExecutionDate { get; set; } = DateTime.Now;

        [ForeignKey("Author")]
        public int? AuthorId { get; set; } = null;
        public UserModel? Author { get; set; }
    }
}