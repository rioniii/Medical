﻿using Microsoft.EntityFrameworkCore;

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
        public DbSet<Pacienti> Pacienti{ get; set; }

    }
}
