using System.Data;

namespace ReactApp1.Server.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; } 
        public DateTime DateOfBirth { get; set; } 

        // Foreign key for Role
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
