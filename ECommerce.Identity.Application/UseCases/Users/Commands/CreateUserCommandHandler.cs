using CSharpFunctionalExtensions;
using ECommerce.Domain.Repositories;
using ECommerce.Domain.Specifications;
using MediatR;
using ECommerce.Domain.Entities;
using ECommerce.Application.Services;
using ECommerce.Domain.Common;
using ECommerce.Domain.Errors;

namespace ECommerce.Application.UseCases.Users.Commands
{
    public class CreateUserCommandHandler
        (IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, IExecutionResult<Guid>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IExecutionResult<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existUserByEmail = await _userRepository.AnyAsync(UserSpecification.ByEmail(request.Email), cancellationToken);
            if (existUserByEmail)
                return ExecutionResult.Failure<Guid>(UserErrors.ExistByEmail());

            var roles = await _roleRepository.GetList(RoleSpecification.ByIds(request.RoleIds), cancellationToken);
            if (!roles.Any())
                return ExecutionResult.Failure<Guid>(UserErrors.RolesNotFound());

            var passwordHash = _passwordHasher.GenerateHash(request.Password);

            var userResult = User.Create(Guid.NewGuid(), request.UserName, passwordHash, request.UserType, 
                                         request.Email, request.FirstName, request.LastName, roles.ToList());

            if (userResult.IsFailure)
                return ExecutionResult.Failure<Guid>(userResult.Error);

            await _userRepository.InsertAsync(userResult.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success(userResult.Value.Id);
        }
    }
}
