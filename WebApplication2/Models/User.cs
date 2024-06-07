using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SdSystem.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public Role Role { get; set; }
    }

    public enum Role
    {
        Admin,
        Coordinator,
        Support,
        Employee
    }

    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; }
    }
}