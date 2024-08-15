using System.ComponentModel.DataAnnotations;

namespace ECommerce.Api.Contracts
{
    public record RemoveRolesForUserRequest([Required]Guid UserId, [Required] Guid[] RoleIds);
}
