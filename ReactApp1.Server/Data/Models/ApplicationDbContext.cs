using System.Numerics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ReactApp1.Server.Data.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Mjeku> Mjeket { get; set; }
        public DbSet<Pacienti> Pacientet { get; set; }
        public DbSet<Termini> Terminet { get; set; }
        public DbSet<Historiku> Historiqet { get; set; }
        public DbSet<Fatura> Faturat { get; set; }
        public DbSet<Sherbimi> Sherbimet { get; set; }
        public DbSet<Dhoma> Dhomat { get; set; }
        public DbSet<DhomaPacientit> DhomaPacienteve { get; set; }
        public DbSet<JWT> JWTs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey });

            // Many-to-one relationship between User and Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            // One-to-one relationship between Doctor and User
            modelBuilder.Entity<Mjeku>()
                .HasOne(d => d.User)
                .WithOne()
                .HasForeignKey<Mjeku>(d => d.UserId);

            // One-to-one relationship between Patient and User
            modelBuilder.Entity<Pacienti>()
                .HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Pacienti>(p => p.UserId);

            // Many-to-many relationship between Doctor and Appointments
            modelBuilder.Entity<Termini>()
                .HasOne(a => a.Mjeku)
                .WithMany(d => d.Terminet)
                .HasForeignKey(a => a.DoktorId);

            // Many-to-many relationship between Patient and Appointments
            modelBuilder.Entity<Termini>()
                .HasOne(a => a.Pacienti)
                .WithMany(p => p.Terminet)
                .HasForeignKey(a => a.PacientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-many relationship between Doctor and MedicalRecords
            modelBuilder.Entity<Historiku>()
                .HasOne(mr => mr.Mjeku)
                .WithMany(d => d.Historiks)
                .HasForeignKey(mr => mr.MjekuId);

            // Many-to-many relationship between Patient and MedicalRecords
            modelBuilder.Entity<Historiku>()
                .HasOne(mr => mr.Pacienti)
                .WithMany(p => p.Historiks)
                .HasForeignKey(mr => mr.PacientId)
                .OnDelete(DeleteBehavior.Restrict);


            // One-to-one relationship between User and JWT (for refresh tokens)
            modelBuilder.Entity<JWT>()
                .HasOne(j => j.User)
                .WithOne()
                .HasForeignKey<JWT>(j => j.UserId);

            // Specify precision and scale for the Cmimi property in Sherbimi
            modelBuilder.Entity<Sherbimi>()
                .Property(s => s.Cmimi)
                .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as needed

            // Specify precision and scale for the Shuma property in Fatura
            modelBuilder.Entity<Fatura>()
                .Property(f => f.Shuma)
                .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as needed
        }
    }
}
