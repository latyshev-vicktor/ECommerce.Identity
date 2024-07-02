using MediatR;

namespace ECommerce.Domain.DomainEvents
{
    public class CreatedUserEvent : INotification
    {
        public Guid UserId { get; }
        public string UserName { get; }
        public string Email { get; }

        public CreatedUserEvent(Guid userId, string userName, string email)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
        }
    }
}
