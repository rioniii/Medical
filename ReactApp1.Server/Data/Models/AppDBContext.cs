using Microsoft.EntityFrameworkCore;

namespace ReactApp1.Server.Data.Models
{
    public class AppDBContext : DbContext

    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)

        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRole> UsersRoless { get; set; }
        public DbSet<Pershkrimi> Pershkrimi { get; set; }
        public DbSet<Dhomat> Dhomat { get; set; }
        public DbSet<Faturimi> Faturat { get; set; }
        public DbSet<Contact> Contacti { get; set; }
        public DbSet<Repart> Reparti { get; set; }
        public DbSet<Sherbimet> Sherbimi { get; set; }
        public DbSet<Terminet> Termini { get; set; }
        public DbSet<Pacienti> Pacienti { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-one relationship between Faturimi and Pershkrimi
            modelBuilder.Entity<Faturimi>()
                .HasOne(f => f.Pershkrimi) // Assuming Faturimi has a navigation property named Pershkrimi
                .WithOne(p => p.Faturimet) // Assuming Pershkrimi has a navigation property named Faturimet
                .HasForeignKey<Faturimi>(f => f.Pershkrimi_ID); // Assuming Faturimi has a foreign key property named PershkrimiId
        }
    }
}
