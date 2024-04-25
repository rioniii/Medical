namespace ReactApp1.Server.Data.Models
{
    public class Dhoma
    {
        public int Dhoma_Id { get; }
        public string NrDhoma { get;}
        public int Kapaciteti{get;set;}
        public int NrPacienteve { get;set;}

      
        public int Pacienti_ID { get;  }
        public int Recepcionist_id { get; }
        public List<Recepcionisti> Recepcionistat { get; set; }
        public List<Pacienti> Pacientat { get; set; }
        public int Reparti_Id { get;}
        public Reparti Reparti { get; set; }

    }
}
