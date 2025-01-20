using System.ComponentModel.DataAnnotations;

namespace Main.Standards.Data.Models
{
    public class ServerModel : BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}