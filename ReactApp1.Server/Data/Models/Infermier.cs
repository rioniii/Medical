
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.Data.Models
{
    public class Infermier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Infermier_Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Reparti is required")]
        public string Reparti { get; set; }

        [Required(ErrorMessage = "Angazhimet is required")]
        public String Angazhimet { get; set; }

        
        public Pershkrimi Pershkrimet { get; set; }




    }
}
