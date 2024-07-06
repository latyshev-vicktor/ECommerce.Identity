using CSharpFunctionalExtensions;
using ECommerce.Domain.Repositories;
using ECommerce.Domain.Specifications;
using MediatR;
using ECommerce.Domain.Entities;
using ECommerce.Application.Services;

namespace ECommerce.Application.UseCases.Users.Commands
{
    public class CreateUserCommandHandler
        (IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, Result<Guid>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existUserByEmail = await _userRepository.AnyAsync(UserSpecification.ByEmail(request.Email), cancellationToken);
            if (existUserByEmail)
                return Result.Failure<Guid>("Пользователь с таким email уже существует");

            var roles = await _roleRepository.GetList(RoleSpecification.ByIds(request.RoleIds), cancellationToken);
            if (!roles.Any())
                return Result.Failure<Guid>("Перечень ролей по переданным идентификаторам не найдены");

            var passwordHash = _passwordHasher.GenerateHash(request.Password);

            var userResult = User.Create(Guid.NewGuid(), request.UserName, passwordHash, request.UserType, 
                                         request.Email, request.FirstName, request.LastName, roles.ToList());

            if (userResult.IsFailure)
                return Result.Failure<Guid>(userResult.Error);

            await _userRepository.InsertAsync(userResult.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return userResult.Value.Id;
        }
    }
}
