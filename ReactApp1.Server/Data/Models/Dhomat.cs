using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactApp1.Server.Data.Models
{
    public class Dhomat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Dhoma_Id { get; set; }
        [Required(ErrorMessage = "Numri i dhomes is required!")]
        public string NrDhoma { get; set; }
        [Required(ErrorMessage = "Kapaciteti is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "Kapaciteti must be a positive value")]
        public int Kapaciteti { get; set; }
        [Required(ErrorMessage = "Numri Pacienteve is required!")]
        [Range(0, int.MaxValue, ErrorMessage = "Numri i Pacienteve must be a positive value")]
        public int NrPacienteve { get; set; }


        public int? Pacienti_ID { get; }
        public int? Reparti_Id { get; }
        public Repart Repart { get; set; }
        public List<Pacienti> Pacientat { get; set; }



    }
}