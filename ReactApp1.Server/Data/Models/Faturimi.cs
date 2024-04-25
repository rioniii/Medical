namespace ReactApp1.Server.Data.Models
{
    public class Faturimi
    {
        public int Fatura_Id { get; set; }
        public double Shuma { get; set; }
        public string Data { get; set; }
        public string Statusi{ get; set; }

        public int Pacienti_Id { get; set; }

        public List<Pacienti> Pacientat { get; set; }

    }
}
