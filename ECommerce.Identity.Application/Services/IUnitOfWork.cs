using Microsoft.EntityFrameworkCore.Storage;

namespace ECommerce.Application.Services
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<IDbContextTransaction> BeginTransaction(CancellationToken cancel);

        Task CommitTransaction(CancellationToken cancel);

        Task RollbackTransaction(CancellationToken cancel);
    }
}
