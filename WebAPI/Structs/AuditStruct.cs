using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models;

namespace WebAPI.Structs
{
    public class AuditStruct
    {
        public DateTime CreationDate { get; set; }

        [ForeignKey("CreatedBy")]
        public int? Id_CreatedBy { get; set; }
        public UserModel? CreatedBy { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        [ForeignKey("LastUpdateBy")]
        public int? Id_LastUpdateBy { get; set; }
        public UserModel? LastUpdateBy { get; set; }
    }
}