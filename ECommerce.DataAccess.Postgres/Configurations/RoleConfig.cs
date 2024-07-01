using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.DataAccess.Postgres.Configurations
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();

            builder.HasMany(x => x.Permissions)
                .WithMany(x => x.Roles)
                .UsingEntity<RolePermission>(
                    l => l.HasOne(x => x.Permission).WithMany().HasForeignKey(x => x.PermissionId),
                    r => r.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId),
                    k => k.HasKey(x => new {x.RoleId, x.PermissionId})
                );
        }
    }
}
