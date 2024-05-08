﻿using Microsoft.EntityFrameworkCore;

namespace ReactApp1.Server.Data.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure one-to-one relationship between Faturimi and Pershkrimi entities
            modelBuilder.Entity<Faturimi>()
                .HasOne(f => f.Pershkrimi)
                .WithOne(p => p.Faturimet)
                .HasForeignKey<Pershkrimi>(p => p.Faturimi_Id);

            modelBuilder.Entity<Dhoma>().HasKey(d => d.Dhoma_Id);
            modelBuilder.Entity<Faturimi>().HasKey(d => d.Fatura_Id);
            modelBuilder.Entity<Infermier>().HasKey(d => d.Infermier_Id);
            modelBuilder.Entity<Mjeku>().HasKey(d => d.Mjeku_Id);
            modelBuilder.Entity<Pacienti>().HasKey(d => d.Patient_Id);
            modelBuilder.Entity<Pershkrimi>().HasKey(d => d.Pershkrimi_Id);
            modelBuilder.Entity<Recepcionisti>().HasKey(d => d.Recepcionisti_Id);
            modelBuilder.Entity<Contact>().HasKey(d => d.Contact_Id);
            modelBuilder.Entity<Pershkrimi>().HasKey(d => d.Pershkrimi_Id);
            modelBuilder.Entity<Sherbimet>().HasKey(d => d.Sherbimet_Id);
            modelBuilder.Entity<Reparti>().HasKey(d => d.Reparti_Id);
            modelBuilder.Entity<Termini>().HasKey(d => d.Termini_Id);
            modelBuilder.Entity<User>().HasKey(d => d.Id);
     
            



        }
        public DbSet<Pacienti> Pacienti { get; set; }
        public DbSet<Mjeku> Mjeku { get; set; }
        public DbSet<Infermier> Infermier { get; set; }
        public DbSet<Recepcionisti> Recepcionisti { get; set; }
        public DbSet<Dhoma> Dhoma { get; set; }
        public DbSet<Faturimi> Faturimi { get; set; }
        public DbSet<Pershkrimi> Pershkrimi { get; set; }
        public DbSet<Reparti> Reparti { get; set; }
        public DbSet<Sherbimet> Sherbimet { get; set; }
        public DbSet<Termini> Termini { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
