using ECommerce.Domain.DTO.Permission;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.DTO.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public bool IsBlocked { get; set; }
        public string UserType { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PermissionDto[] Permissions { get; set; } = Array.Empty<PermissionDto>();
    }
}
