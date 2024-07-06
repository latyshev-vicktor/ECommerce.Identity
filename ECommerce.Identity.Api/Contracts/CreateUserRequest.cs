namespace ECommerce.Api.Contracts
{
    public record class CreateUserRequest(
        string UserName,
        string Email,
        string FirstName,
        string LastName,
        string Password,
        int UserType,
        Guid[] RoleIds);
}
