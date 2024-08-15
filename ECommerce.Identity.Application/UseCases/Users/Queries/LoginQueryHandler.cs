using ECommerce.Application.Services;
using ECommerce.Domain.Common;
using ECommerce.Domain.DTO;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Errors;
using ECommerce.Domain.Repositories;
using ECommerce.Domain.Specifications;
using MediatR;

namespace ECommerce.Application.UseCases.Users.Queries
{
    public class LoginQueryHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<LoginQuery, IExecutionResult<TokenDto>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IExecutionResult<TokenDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserWithPermissions(UserSpecification.ByEmail(request.Email), cancellationToken);

            if (user == null)
                return ExecutionResult.Failure<TokenDto>(UserErrors.NotCorrentEmailOrPassword());

            var validPassword = _passwordHasher.VerifyPassword(request.Password, user.Password);

            if (!validPassword)
                return ExecutionResult.Failure<TokenDto>(UserErrors.NotCorrentEmailOrPassword());

            var accessToken = _jwtProvider.GenerateJwtToken(user);
            var refreshTokenValue = _jwtProvider.GenerateRefreshToken();

            if (!user.RefreshTokens.Any(x => x.IsRevoked))
                _jwtProvider.RemoveRefreshToken(user);

            var newRefreshToken = new RefreshToken(user.Id, refreshTokenValue, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(15));

            user.AddRefreshToken(newRefreshToken);
            _userRepository.Update(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success(new TokenDto(accessToken, refreshTokenValue));
        }
    }
}
