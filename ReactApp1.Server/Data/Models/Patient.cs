namespace ReactApp1.Server.Data.Models
{
    public class Patient
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Boolean isRegistered { get; set; }
        public int? age { get; set; }

    }
}
