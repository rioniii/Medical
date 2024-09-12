using System.Numerics;

namespace ReactApp1.Server.Data.Models
{
    public class Termini
    {
            public int Id { get; set; }

            // Foreign key for Doctor
            public int DoktorId { get; set; }
            public Mjeku Mjeku { get; set; }

            // Foreign key for Patient
            public int PacientId { get; set; }
            public Pacienti Pacienti { get; set; }

            public DateTime DataTerminit { get; set; }
            public string Statusi { get; set; } // e.g., "Scheduled", "Completed"
    }
}
