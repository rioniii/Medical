using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactApp1.Server.Data.Models
{
    public class Pershkrimi
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Pershkrimi_Id { get; set; }
        [Required(ErrorMessage ="Anamneza is required")]
        public string Anamneza_Statusi { get; set; }

        [Required(ErrorMessage = "Ekzaminimi is required")]
        public string Ekzaminimi { get; set; }

        [Required(ErrorMessage = "Diagnoza is required")]
        public string Diagnoza {  get; set; }
        [Required(ErrorMessage = "Terapia is required")]
        public string Terapia { get; set; }
        [Required(ErrorMessage = "Perfundimi is required")]
        public string Perfundimi { get; set; }


 /*       public int Faturimi_Id {  get; set; }
       
        public  Faturat Faturimet { get; set; }*/
       
    }
}
