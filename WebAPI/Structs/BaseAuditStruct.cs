using Setia.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setia.Structs
{
    public class BaseAuditStruct
    {
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [ForeignKey("CreatedBy")]
        public int? Id_CreatedBy { get; set; }
        public UserModel? CreatedBy { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        [ForeignKey("LastUpdateBy")]
        public int? Id_LastUpdateBy { get; set; }
        public UserModel? LastUpdateBy { get; set; }
    }
}