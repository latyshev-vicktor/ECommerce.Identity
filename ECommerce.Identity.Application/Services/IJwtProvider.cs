using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services
{
    public interface IJwtProvider
    {
        string GenerateJwtToken(User user);
        string GenerateRefreshToken();
        void RemoveRefreshToken(User user);
    }
}
