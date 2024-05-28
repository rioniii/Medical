using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactApp1.Server.Data.Models
{
    public class Faturimi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Fatura_Id { get; set; }

        [Required(ErrorMessage = "Shuma is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Shuma must be a positive value")]
        public double Shuma { get; set; }
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Statusi is required")]
        public string Statusi { get; set; }

        public int? Pershkrimi_ID { get; }

        public Pershkrimi Pershkrimi { get; set; }

    }
}
