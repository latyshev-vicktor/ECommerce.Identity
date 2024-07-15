using System.ComponentModel.DataAnnotations;

namespace ECommerce.Api.Contracts
{
    public record class LoginRequest([Required]string Email, [Required]string Password);
}
