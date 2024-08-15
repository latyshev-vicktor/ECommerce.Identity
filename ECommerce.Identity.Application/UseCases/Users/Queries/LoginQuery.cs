using CSharpFunctionalExtensions;
using ECommerce.Domain.Common;
using ECommerce.Domain.DTO;
using MediatR;

namespace ECommerce.Application.UseCases.Users.Queries
{
    public record class LoginQuery(string Email, string Password) : IRequest<IExecutionResult<TokenDto>>;
}
