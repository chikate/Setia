using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Coins { get; set; }
        public DateTime CreationDate { get; set; }
    }
}