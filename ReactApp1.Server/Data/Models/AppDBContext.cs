using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Migrations;

namespace ReactApp1.Server.Data.Models
{
    public class AppDBContext : IdentityDbContext<ApplicationUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Pershkrimi> Pershkrimi { get; set; }
        public DbSet<Dhomat> Dhomat { get; set; }
        public DbSet<Faturimi> Faturat { get; set; }
        public DbSet<Contact> Contacti { get; set; }
        public DbSet<Repart> Reparti { get; set; }
        public DbSet<Sherbimet> Sherbimi { get; set; }
        public DbSet<Terminet> Termini { get; set; }
        public DbSet<Roli> Rolis { get; set; }
        public DbSet<Userr> Userrs { get; set; }
        public DbSet<UsersRoles> UsersRoless { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Faturimi>()
                .HasOne(f => f.Pershkrimi)
                .WithOne(p => p.Faturimet)
                .HasForeignKey<Faturimi>(f => f.Pershkrimi_ID);

        }
    }
}
