using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactApp1.Server.Data.Models
{
    public class Faturimi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Fatura_Id { get; set; }

        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Statusi is required")]
        public string Statusi { get; set; }

        public int? Pershkrimi_ID { get; }

        public Pershkrimi Pershkrimi { get; set; }

        [NotMapped]
        public double TotalOrderPrice { get; private set; }

        // Method to calculate total order price
        public void CalculateTotalOrderPrice()
        {
            TotalOrderPrice = 0;
            foreach (var item in OrderItems)
            {
                TotalOrderPrice += item.Price * item.Quantity;

            }
        }
    }
}

