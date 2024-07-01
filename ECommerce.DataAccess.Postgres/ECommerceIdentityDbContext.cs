using Microsoft.EntityFrameworkCore;
using ECommerce.Domain.Entities;
using ECommerce.DataAccess.Postgres.Configurations;

namespace ECommerce.DataAccess.Postgres
{
    public class ECommerceIdentityDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        public ECommerceIdentityDbContext(DbContextOptions<ECommerceIdentityDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new PermissionConfig());
            modelBuilder.ApplyConfiguration(new RoleUserConfig());
            modelBuilder.ApplyConfiguration(new RolePermissionConfig());
        }
    }
}
