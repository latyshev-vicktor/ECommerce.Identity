using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.UseCases.Users.Commands
{
    public record class CreateUserCommand(
        string UserName,
        string FirstName,
        string LastName,
        string Email,
        string Password,
        int UserType,
        Guid[] RoleIds) : IRequest<Result<Guid>>;
}
