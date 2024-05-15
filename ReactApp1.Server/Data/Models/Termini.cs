using Microsoft.Identity.Client;

namespace ReactApp1.Server.Data.Models
{
    public class Termini
    {
        public int Termini_Id { get; }

        public string Statusi { get; set; }
        public int Data { get; set; }

        public int Pacienti_Id { get; set; }
     //  public Pacienti Pacienti { get; set; }


    }
}
