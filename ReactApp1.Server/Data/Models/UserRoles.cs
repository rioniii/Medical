using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.Data.Models
{
    public class UserRoles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsersRolesId { get; set; }

        public int UsersId { get; set; }
        public Userr Userr { get; set; }

        public int RolesId { get; set; }
        public Roli Roles { get; set; } 
    }
}
