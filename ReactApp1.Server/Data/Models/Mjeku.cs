namespace ReactApp1.Server.Data.Models
{
    public class Mjeku
    {
        public int Mjeku_Id { get; }

        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public string Reparti { get; set; }
        public string Specializimi {  get; set; }
        public string Nderrimi { get; set; }
        public string Angazhimi { get; set; }

        public int Termini_Id { get; }
        public int Recepcionisti_Id { get; }

        public List<Termini> Terminet  { get; set; }

        public List<Recepcionisti> Recepcionistat { get; set; }





    }
}
