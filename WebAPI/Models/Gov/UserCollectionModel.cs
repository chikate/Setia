using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setia.Models.Gov
{
    public class UserCollectionModel : BaseModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("Post")]
        public Guid? PostId { get; set; }
        public PostModel? PostData { get; set; }
    }
}