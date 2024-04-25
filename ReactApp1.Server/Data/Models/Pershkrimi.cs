namespace ReactApp1.Server.Data.Models
{
    public class Pershkrimi
    {
        public int Pershkrimi_Id { get; set; }
        public string Anamneza_Statusi { get; set; }
        public string Ekzaminimi { get; set; }
        public string Diagnoza {  get; set; }
        public string Terapia { get; set; }
        public string Perfundimi { get; set; }

        public int Pacient_Id { get; set; }
        public int Faturimi {  get; set; }
        public int Mjeku_Id { get; set; }


        public Faturimi Faturimi { get; set; }
        public Pacienti Pacienti { get; set; }
        public Mjeku Mjeku { get; set; }
    }
}
