using ECommerce.Application.Services;
using ECommerce.Domain.Common;
using ECommerce.Domain.Errors;
using ECommerce.Domain.Repositories;
using ECommerce.Domain.Specifications;
using MediatR;

namespace ECommerce.Application.UseCases.Users.Commands
{
    public class RemoveRolesForUserCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<RemoveRolesForUserCommand, IExecutionResult>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IExecutionResult> Handle(RemoveRolesForUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstOrDefault(UserSpecification.ById(request.UserId), cancellationToken, x => x.Roles);
            if (user == null)
                return ExecutionResult.Failure(UserErrors.NotFoundById());

            var roles = await _roleRepository.GetList(RoleSpecification.ByIds(request.RoleIds), cancellationToken);
            if (!roles.Any())
                return ExecutionResult.Failure(UserErrors.RolesNotFound());

            user.RemoveRoles(roles.ToList());

            _userRepository.Update(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success();
        }
    }
}
