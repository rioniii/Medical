using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.Data.Models
{
    public class UserRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsersRolesId { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int RoleId { get; set; }

        public Roles Roles { get; set; }

    }
}
