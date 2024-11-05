using Microsoft.AspNetCore.Identity;
using System;

namespace ReactApp1.Server.Data.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; } = DateTime.Now;
        public DateTime TokenExpires { get; set; }

        // Foreign key for Role (assuming you need a custom RoleId)
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
