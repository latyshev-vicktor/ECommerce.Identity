using CSharpFunctionalExtensions;
using ECommerce.Application.Services;
using ECommerce.DataAccess.Postgres;
using ECommerce.Domain.SeedWorks;
using MediatR;

namespace ECommerce.Infrastructure.Impl.Services
{
    public class UnitOfWork(ECommerceIdentityDbContext dbContext, IPublisher publisher) : IUnitOfWork
    {
        private readonly ECommerceIdentityDbContext _dbContext = dbContext;
        private readonly IPublisher _publisher = publisher;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            await PublishDomainEvent();

            return result;
        }

        private async Task PublishDomainEvent()
        {
            var domainEntities = _dbContext.ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            foreach (var item in domainEntities)
            {
                item.Entity.ClearDomainEvents();
            }

            foreach (var domainEvent in domainEvents)
                await _publisher.Publish(domainEvent);
        }
    }
}
