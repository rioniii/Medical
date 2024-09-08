﻿namespace ReactApp1.Server.Data.Models
{

    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expired { get; set; }

    }
}