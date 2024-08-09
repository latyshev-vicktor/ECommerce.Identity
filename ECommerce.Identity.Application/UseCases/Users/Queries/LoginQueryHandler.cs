using ECommerce.Application.Services;
using ECommerce.Domain.Common;
using ECommerce.Domain.Errors;
using ECommerce.Domain.Repositories;
using ECommerce.Domain.Specifications;
using MediatR;

namespace ECommerce.Application.UseCases.Users.Queries
{
    public class LoginQueryHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider) : IRequestHandler<LoginQuery, IExecutionResult<string>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task<IExecutionResult<string>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserWithPermissions(UserSpecification.ByEmail(request.Email), cancellationToken);

            if (user == null)
                return ExecutionResult.Failure<string>(UserErrors.NotCorrentEmailOrPassword());

            var validPassword = _passwordHasher.VerifyPassword(request.Password, user.Password);

            if (!validPassword)
                return ExecutionResult.Failure<string>(UserErrors.NotCorrentEmailOrPassword());

            var token = _jwtProvider.GenerateJwtToken(user);
            return ExecutionResult.Success(token);
        }
    }
}
