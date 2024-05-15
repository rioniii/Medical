using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.Data.Models
{
    public class Roles
    {

            public int Id { get; set; }

            [Required(ErrorMessage = "Role name is required")]
            public string Emri { get; set; }

            [Required(ErrorMessage = "Paga is required")]
            [Range(0, double.MaxValue, ErrorMessage = "Paga must be a positive value")]
            public decimal Paga { get; set; }

            [Required(ErrorMessage = "Reparti is required")]
            public string Reparti { get; set; }

            [Required(ErrorMessage = "Specializimi is required")]
            public string Specializimi { get; set; }

            [Required(ErrorMessage = "Nderrimi is required")]
            public string Nderrimi { get; set; }

            [Required(ErrorMessage = "Angazhimi is required")]
            public string Angazhimi { get; set; }
            public DateTime RoleStartDate { get; set; }

            public DateTime? RoleEndDate { get; set; }

            //public List<UsersRoles> UserRoles { get; set; }
        }
    }