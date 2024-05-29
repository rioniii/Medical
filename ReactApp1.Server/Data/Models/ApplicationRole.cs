using Microsoft.AspNetCore.Identity;

namespace ReactApp1.Server.Data.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }

    }
}