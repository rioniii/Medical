namespace ReactApp1.Server.Data.Models
{
    public class Recepcionisti
    {
        public string Recepcionisti_Id { get;}
        public string Emri { get; set; }


        
        public int Mjeku_Id { get;}
        public int Pacienti_Id { get;}
/*        public int Reparti_Id { get;}
        public string Dhoma_Id { get;}*/
    
        public Mjeku Mjeku { get; set; }
        public List<Pacienti>  Pacientet { get; set; }
/*        public Reparti Reparti { get; set; }
        public Dhoma Dhoma { get; set; }*/




    }
}
