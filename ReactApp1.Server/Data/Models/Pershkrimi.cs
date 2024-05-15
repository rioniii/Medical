using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactApp1.Server.Data.Models
{
    public class Pershkrimi
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Pershkrimi_Id { get; set; }
        public string Anamneza_Statusi { get; set; }
        public string Ekzaminimi { get; set; }
        public string Diagnoza {  get; set; }
        public string Terapia { get; set; }
        public string Perfundimi { get; set; }


 /*       public int Faturimi_Id {  get; set; }
       
        public  Faturat Faturimet { get; set; }*/
       
    }
}
