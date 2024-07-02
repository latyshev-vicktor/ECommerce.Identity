using MediatR;

namespace ECommerce.Domain.SeedWorks
{
    public class BaseEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedDate { get; private set; }
        public DateTimeOffset ModifyDate { get; private set; }

        private List<INotification> _domainEvents = [];
        public IReadOnlyList<INotification> DomainEvents => _domainEvents;

        public void AddDomainEvent(INotification eventIntem)
        {
            _domainEvents.Add(eventIntem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public void MakeModify()
        {
            ModifyDate = DateTimeOffset.Now;
        }
    }
}
