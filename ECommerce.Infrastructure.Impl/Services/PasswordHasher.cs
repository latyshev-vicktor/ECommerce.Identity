using ECommerce.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Impl.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string GenerateHash(string password)
            => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        public bool VerifyPassword(string password, string hashPassword)
            => BCrypt.Net.BCrypt.EnhancedVerify(password, hashPassword);
    }
}
