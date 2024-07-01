using Microsoft.EntityFrameworkCore;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.DataAccess.Postgres.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.ComplexProperty(x => x.Email, options =>
            {
                options.IsRequired();
                options.Property(o => o.Value).HasColumnName(nameof(User.Email));
            });

            builder.ComplexProperty(x => x.FullName, options =>
            {
                options.IsRequired();
                options.Property(o => o.FirstName).HasColumnName(nameof(User.FullName.FirstName));
                options.Property(o => o.LastName).HasColumnName(nameof(User.FullName.LastName));
            });

            builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.Password).IsRequired();

            builder.HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<RoleUser>(
                    l => l.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId),
                    r => r.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId),
                    k => k.HasKey(x => new {x.UserId, x.RoleId}));
        }
    }
}
