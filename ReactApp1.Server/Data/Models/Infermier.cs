namespace ReactApp1.Server.Data.Models
{
    public class Infermier
    {
        public int Infermier_Id {  get;  }
        public string Emri {  get; set; }
        public string Mbiemri { get; set; }
        public string Reparti { get; set; }
        public string Nderrimi { get; set; }
        public int Pershkrimi_Id {  get; set; }
        public List<Pershkrimi> Pershkrimet {  get; set; }


    }
}
