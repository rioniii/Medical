namespace ReactApp1.Server.Data.Models
{
    public class DhomaPacientit
    {
        public int Id { get; set; }

        // Foreign key for Patient
        public int PacientId { get; set; }
        public Pacienti Pacienti{ get; set; }

        // Foreign key for Room
        public int DhomaId { get; set; }
        public Dhoma Dhoma{ get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
    }
}
