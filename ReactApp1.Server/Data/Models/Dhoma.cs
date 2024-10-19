namespace ReactApp1.Server.Data.Models
{
    public class Dhoma
    {
       
           // public int Id { get; set; }
            public string NrDhomes { get; set; }
            public string Lloji_Dhomes { get; set; } // e.g., "ICU", "General", "Private"
            public int Kapaciteti { get; set; }
            public bool Available { get; set; }

            public List<DhomaPacientit> DhomaPacienteve { get; set; }
     
    }
}
