using ECommerce.Application.Options;
using ECommerce.Application.Services;
using ECommerce.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

            var signinCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signinCredentials,
                issuer: _jwtOptions.Issuer,
                expires: DateTime.UtcNow.AddHours(_jwtOptions.Expired)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
