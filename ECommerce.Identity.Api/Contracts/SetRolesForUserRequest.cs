using System.ComponentModel.DataAnnotations;

namespace ECommerce.Api.Contracts
{
    public record SetRolesForUserRequest([Required]Guid UserId, [Required]Guid[] RoleIds);
}
