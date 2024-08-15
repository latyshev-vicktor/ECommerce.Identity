using ECommerce.DataAccess.Postgres;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Repositories;

namespace ECommerce.Infrastructure.Impl.Repositories
{
    public class RefreshTokenRepository(ECommerceIdentityDbContext context) : IRefreshTokenRepository
    {
        private readonly ECommerceIdentityDbContext _context = context;

        public async Task Insert(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken, new CancellationToken());
        }

        public void Update(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);
        }
    }
}
