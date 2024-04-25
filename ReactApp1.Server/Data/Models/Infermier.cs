namespace ReactApp1.Server.Data.Models
{
    public class Infermier
    {
        public int Infermier_id {  get;  }
        public string Emri {  get; set; }
        public string Mbiemri { get; set; }

        public string Reparti { get; set; }
        public string Nderrimi { get; set; }
        public int Pershkrimi_Id {  get; set; }
        public Pershkrimi Pershkrimi {  get; set; }

    }
}
