using Microsoft.EntityFrameworkCore;
using PermissionManagerAPI.Configurations;

namespace PermissionManagerAPI.Models
{
    public class UserManagerContext : DbContext 
    {
        public UserManagerContext(DbContextOptions<UserManagerContext> options) : base(options) 
        {

        }

        public DbSet<Permission>? Permissions { get; set; }
        public DbSet<PermissionType>? PermissionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PermissionTypeConfiguration());
        }
    }
}
