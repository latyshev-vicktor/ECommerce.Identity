using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.DataAccess.Postgres.Configurations
{
    public class RefreshTokenConfig : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User)
                .WithMany(x => x.RefreshTokens)
                .HasForeignKey(x => x.UserId);

            builder.Property(x => x.TokenHash).IsRequired();
            builder.Ignore(x => x.IsExpired);
            builder.Ignore(x => x.IsRevoked);
        }
    }
}
