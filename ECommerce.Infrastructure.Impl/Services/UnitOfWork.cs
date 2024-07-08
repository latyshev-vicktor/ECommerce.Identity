using CSharpFunctionalExtensions;
using ECommerce.Application.Services;
using ECommerce.DataAccess.Postgres;
using ECommerce.Domain.SeedWorks;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;

namespace ECommerce.Infrastructure.Impl.Services
{
    public class UnitOfWork(ECommerceIdentityDbContext dbContext, IPublisher publisher) : IUnitOfWork
    {
        private readonly ECommerceIdentityDbContext _dbContext = dbContext;
        private readonly IPublisher _publisher = publisher;

        public async Task<IDbContextTransaction> BeginTransaction(CancellationToken cancel)
        {
            return await _dbContext.Database.BeginTransactionAsync(cancel);
        }

        public async Task CommitTransaction(CancellationToken cancel)
        {
            await _dbContext.Database.CommitTransactionAsync(cancel);
        }

        public async Task RollbackTransaction(CancellationToken cancel)
        {
            await _dbContext.Database.RollbackTransactionAsync(cancel);
        }

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
