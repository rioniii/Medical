using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.Data.Models
{
    public class Sherbimet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Sherbimet_Id { get; set; }
        [Required(ErrorMessage = "Sherbimi is required")]
        public string Sherbimi { get; set; }
        [Required(ErrorMessage = "Cmimi is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Cmimi must be a positive value!!")]
        public double Cmimi { get; set; }

        public int Fatura_Id { get; }
        
        public List<Faturimi> Faturat { get; set; }
    }
}
