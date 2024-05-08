namespace ReactApp1.Server.Data.Models
{
    public class Dhoma
    {
        public int Dhoma_Id { get;set; }
        public string NrDhoma { get; set; }
        public int Kapaciteti{get;set;}
        public int NrPacienteve { get;set;}

      
        public int Pacienti_ID { get;  }
        public int Reparti_Id { get;}
        public int Recepcionist_id { get; }
        public Reparti Reparti { get; set; }
        public List<Recepcionisti> Recepcionistat { get; set; }
        public List<Pacienti> Pacientat { get; set; }

    }
}
