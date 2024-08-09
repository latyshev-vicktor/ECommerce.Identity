using ECommerce.Domain.DTO.User;
using ECommerce.Domain.Repositories;
using MediatR;

namespace ECommerce.Application.UseCases.Users.Queries
{
    public class GetAllUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetAllUsersQuery, IReadOnlyList<UserDto>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<IReadOnlyList<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsersWithPermissions(cancellationToken);

            return users;
        }
    }
}
