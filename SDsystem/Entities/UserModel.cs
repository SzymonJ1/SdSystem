using System.ComponentModel.DataAnnotations;

namespace SDsystem.Entities
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
