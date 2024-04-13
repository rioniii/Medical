namespace ReactApp1.Server.Data.Models
{
    public class Staf
    {
        public int Id { get; set; }
        public string Emri { get; set; }    
        public string Mbiemri { get; set; }
        public int Mosha { get; set; }
        public string Qyteti { get; set; } 
        public string NrTelefonit { get; set; } 
        public string NiveliEdukimit { get; set; }
        public string Specializimi { get; set; }
        public string Roli { get; set; }
        public double Paga { get; set; }
        public string Image { get; set; } = string.Empty;
    }
}
