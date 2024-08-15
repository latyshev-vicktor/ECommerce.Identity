using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task Insert(RefreshToken refreshToken);
        void Update(RefreshToken refreshToken);
    }
}
