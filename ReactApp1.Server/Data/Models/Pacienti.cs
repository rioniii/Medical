namespace ReactApp1.Server.Data.Models
{
    public class Pacienti
    {
        public int Patient_Id { get;}
        public string Emri { get; set; }
        public string Mbiemri { get; set;}
        public char Gjinia { get;}
        public string VitiLindjes{ get; set;}
        public string Vendbanimi { get; set;}
        public string Alergjite { get; set;}
        public string NumriTel {  get; set;}
        public string Pranimi { get; set;}
            
        public int Termini_Id { get;}
        public int Recepcionisti_Id { get; set;}
        public int Faturimi_Id { get;}
        public int Dhoma_Id { get; set; }
        public List<Pacienti> Pacientet { get; set; }
        public List<Termini> Terminet { get; set;}
        public  Recepcionisti Recepcionisti { get; set;}
        public  Faturimi Faturimi { get; set;}

        public List<Pershkrimi> Pershkrimi{ get; set; }



    }
}
