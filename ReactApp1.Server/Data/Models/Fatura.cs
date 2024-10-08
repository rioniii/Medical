﻿namespace ReactApp1.Server.Data.Models
{
    public class Fatura
    {
            public int Id { get; set; }

            // Foreign key for Patient
            public int PacientId { get; set; }
            public Pacienti Pacienti { get; set; }

            // Foreign key for Service
            public int SherbimiId { get; set; }
            public Sherbimi Sherbimi { get; set; }

            public decimal Shuma { get; set; }
            public DateTime Data { get; set; }
            public bool? Paguar { get; set; }
    }
}
