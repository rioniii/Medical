namespace ReactApp1.Server.Data.Models
{
    public class Reparti
    {
        public int Reparti_Id { get;}

        public string Emri { get; set; }
        public int Kati { get; set; }
        public int NrDhomave { get; set; }
        public int Mjeku_Id { get; set; }
        public List<Mjeku> Mjeket {  get; set; }
        public int Dhoma_Id { get; }
        public List<Dhoma> Dhomat {  get; set; }
    }
}
