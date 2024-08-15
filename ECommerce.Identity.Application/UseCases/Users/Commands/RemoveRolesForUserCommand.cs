using ECommerce.Domain.Common;
using MediatR;

namespace ECommerce.Application.UseCases.Users.Commands
{
    public record RemoveRolesForUserCommand(Guid UserId, Guid[] RoleIds) : IRequest<IExecutionResult>;
}
