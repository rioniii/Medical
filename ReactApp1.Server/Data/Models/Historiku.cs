using System.Numerics;

namespace ReactApp1.Server.Data.Models
{
    public class Historiku
    {
            public int Id { get; set; }

            // Foreign key for Doctor
            public int MjekuId { get; set; }
            public Mjeku Mjeku { get; set; }

            // Foreign key for Patient
            public int PacientId { get; set; }
            public Pacienti Pacienti { get; set; }

            public DateTime Data { get; set; }
            public string? Anamneza_Statusi { get; set; }
            public string? Ekzaminimi { get; set; }
            public string? Diagnoza {  get; set; }
            public string? Terapia {  get; set; }
            public string? Perfundimi {  get; set; }


    }
}
