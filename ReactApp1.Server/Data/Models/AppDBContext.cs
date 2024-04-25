/*global using Microsoft.EntityFrameworkCore;

namespace ReactApp1.Server.Data.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            public DbSet<Pacienti> Pacienti { get; set; }
        public DbSet<Mjeku> Mjeku { get; set; }
        public DbSet<Infermier> Infermier { get; set; }
        public DbSet<Recepcionisti> Recepcionisti { get; set; }
        public DbSet<Dhoma> Dhoma { get; set; }
        public DbSet<Faturimi> Faturimi { get; set; }
        public DbSet<Pershkrimi> Pershkrimi { get; set; }
        public DbSet<Register> Register { get; set; }
        public DbSet<Reparti> Reparti { get; set; }
        public DbSet<Sherbimet> Sherbimet { get; set; }
        public DbSet<Termini> Termini { get; set; }

        *//*      public DbSet<Pershkrimi> Pershkrimi { get; set; }*//*

    }




}*/

﻿using Microsoft.EntityFrameworkCore;

namespace ReactApp1.Server.Data.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<Pacienti> Pacienti { get; set; }
        public DbSet<Mjeku> Mjeku { get; set; }
        public DbSet<Infermier> Infermier { get; set; }
        public DbSet<Recepcionisti> Recepcionisti { get; set; }
        public DbSet<Dhoma> Dhoma { get; set; }
        public DbSet<Faturimi> Faturimi { get; set; }
        public DbSet<Pershkrimi> Pershkrimi { get; set; }
        public DbSet<Register> Register { get; set; }
        public DbSet<Reparti> Reparti { get; set; }
        public DbSet<Sherbimet> Sherbimet { get; set; }
        public DbSet<Termini> Termini { get; set; }
    }

}