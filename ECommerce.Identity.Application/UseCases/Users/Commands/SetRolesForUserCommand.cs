using CSharpFunctionalExtensions;
using ECommerce.Domain.Common;
using MediatR;

namespace ECommerce.Application.UseCases.Users.Commands
{
    public record SetRolesForUserCommand(Guid UserId, Guid[] RoleIds) : IRequest<IExecutionResult>;
}
