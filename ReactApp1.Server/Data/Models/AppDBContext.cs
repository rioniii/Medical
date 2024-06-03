using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Pacienti> Pacienti { get; set; }
        public DbSet<Mjeku> Mjekat { get; set; }
/*        public DbSet<Recepcionisti> Recepcionistet { get; set; }*/
        public DbSet<Infermier> Infermieret { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Faturimi>()
                .HasOne(f => f.Pershkrimi)
                .WithOne(p => p.Faturimet)
                .HasForeignKey<Faturimi>(f => f.Pershkrimi_ID);

           /* modelBuilder.Entity<Recepcionisti>()
                .HasOne<ApplicationUser>() // Specifikon që ka një ApplicationUser
                .WithOne() // Nuk specifikon një proprietet të kundërt të navigimit
                .HasForeignKey<Recepcionisti>(r => r.UserId); // Specifikon se cili është çelësi i huaj në Recepcionisti*/
        }
    }
}
