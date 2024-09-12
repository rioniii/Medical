namespace ReactApp1.Server.Data.Models
{
    public class Mjeku
    {
        public int Id { get; set; }

        // Foreign key from User
        public int UserId { get; set; }
        public User User { get; set; } // Each doctor is a user

        public string Specializimi { get; set; }
        public string NumriLicences { get; set; }

        public List<Termini> Terminet { get; set; }
        public List<Historiku> Historiks { get; set; }
    }

}
