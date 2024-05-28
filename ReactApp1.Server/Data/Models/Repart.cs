using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.Data.Models
{
    public class Repart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Reparti_Id { get; set; }

        [Required(ErrorMessage = "Emri is required")]
        public string Emri { get; set; }

        [Required(ErrorMessage = "Kati is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Kati must be a positive value!")]
        public int Kati { get; set; }

        [Required(ErrorMessage = "NrDhomave is required")]
        [Range(0, int.MaxValue, ErrorMessage = "NrDhomave must be a positive value!")]
        public int NrDhomave { get; set; }
        
         public int? Dhoma_Id { get; }
         public List<Dhomat> Dhomat { get; set; }

    }
}
