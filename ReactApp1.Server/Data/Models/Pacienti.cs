using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactApp1.Server.Data.Models
{
    public class Pacienti
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Patient_Id { get; set; }

        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public char   Gjinia { get; set; } 
        public DateTime VitiLindjes { get; set; } 
        public string? Vendbanimi { get; set; }
        public string? Alergjite { get; set; }
        public string? NumriTel { get; set; }
        public string? Pranimi { get; set; }

        public int Termini_Id { get; set; }
        public int Recepcionisti_Id { get; set; }
        public int Faturimi_Id { get; set; }
        public int Dhoma_Id { get; set; }
    }
}
