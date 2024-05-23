using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.Data.Models
{
    public class Terminet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Termini_Id { get; set; }

        [Required(ErrorMessage = "Statusi is required")]
        public string Statusi { get; set; }
       
        public DateTime Data { get; set; }

        //public int Pacienti_Id { get; set; }
        //public Pacienti Pacienti { get; set; }
    }
}
