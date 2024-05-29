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
        public string UserId {  get; set; }
        public ApplicationUser User { get; set; }
        public string? Alergjite { get; set; }
        public string? Pranimi { get; set; }
        public List<Terminet> Terminet{ get; set; }
   
    }
}
