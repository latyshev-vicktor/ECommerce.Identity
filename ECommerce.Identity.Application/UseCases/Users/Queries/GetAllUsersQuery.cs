using ECommerce.Domain.DTO.User;
using MediatR;

namespace ECommerce.Application.UseCases.Users.Queries
{
    public class GetAllUsersQuery : IRequest<IReadOnlyList<UserDto>>;
}
