using CSharpFunctionalExtensions;
using ECommerce.Application.Services;
using ECommerce.Domain.Repositories;
using ECommerce.Domain.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.UseCases.Users.Queries
{
    public class LoginQueryHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider) : IRequestHandler<LoginQuery, Result<string>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task<Result<string>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserWithPermissions(UserSpecification.ByEmail(request.Email), cancellationToken);
            if (user == null)
                return Result.Failure<string>("Неправильный email или пароль");

            var validPassword = _passwordHasher.VerifyPassword(request.Password, user.Password);
            if (!validPassword)
                return Result.Failure<string>("Неправильный email или пароль");

            var token = _jwtProvider.GenerateJwtToken(user);

            return Result.Success(token);
        }
    }
}
