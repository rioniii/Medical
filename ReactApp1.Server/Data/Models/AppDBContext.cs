global using Microsoft.EntityFrameworkCore;

namespace ReactApp1.Server.Data.Models
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {
        
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Staf> Stafi { get; set; }
        public DbSet<Repart> Reparti { get; set; }

        public DbSet<Contact> Contacti { get; set; }

    }
   
}
