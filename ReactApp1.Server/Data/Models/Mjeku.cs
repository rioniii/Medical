
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.Data.Models
{
    public class Mjeku
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Mjeku_Id { get; set; }

        public string UserId { get; set; }  
        public ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Reparti is required")]
        public string Reparti { get; set; }

        [Required(ErrorMessage = "Nderrimi is required")]
        public String Nderrimi { get; set; }

        [Required(ErrorMessage = "Angazhimi is required")]
        public String Angazhimi { get; set; }


        
        
    }
}
