using ECommerce.Application.Options;
using ECommerce.Application.Services;
using ECommerce.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ECommerce.DataAccess.Postgres;
using Microsoft.EntityFrameworkCore;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Errors;

namespace ECommerce.Infrastructure.Impl.Services
{
    public class JwtProvider(JwtOptions jwtOptions) : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions = jwtOptions ?? throw new NullReferenceException("Секция JwtOption не может быть null");

        public string GenerateJwtToken(User user)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email.Value),
                new Claim("UserType", user.UserType.ToString())
            ];

            var permissions = user.Roles.SelectMany(x => x.Permissions).Select(x => x.Name).Distinct().ToList();

            foreach(var permission in permissions)
            {
                claims.Add(new Claim("Permissions", permission));
            }

            var claimsIdentity = new ClaimsIdentity(claims);

            var signinCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)), SecurityAlgorithms.HmacSha256);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Issuer = _jwtOptions.Issuer,
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.Expired)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];

            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomBytes);

            var refreshToken = Convert.ToBase64String(randomBytes);

            return refreshToken;
        }

        public void RemoveRefreshToken(User user)
        {
            if (user == null)
                throw new NotFoundException(UserErrors.NotFoundById());

            if (user.RefreshTokens.Any())
            {
                var actualRefreshTokens = user.RefreshTokens.Where(x => !x.IsExpired);
                foreach (var token in actualRefreshTokens)
                    token.Revoked();
            }
        }
    }
}
