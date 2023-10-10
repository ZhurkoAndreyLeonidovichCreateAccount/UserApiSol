using Entities.Configuration;
using Entities.Models;

using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext

    {
        public RepositoryContext(DbContextOptions options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserIdentityConfiguration());
           
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserIdentity> UsersIdentity { get; set; }


    }
}
