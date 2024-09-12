namespace ReactApp1.Server.Data.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } // Doktor, Pacient, Admin
        public List<User> Users { get; set; }
    }
}
