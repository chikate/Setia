using Setia.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setia.Structs
{
    public class BaseAuditStruct
    {
        public DateTime ExecutionDate { get; set; } = DateTime.Now;

        [ForeignKey("Author")]
        public int? Author_Id { get; set; } = null;
        public UserModel? Author { get; set; } = null;
    }
}