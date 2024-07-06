using ECommerce.Domain.Entities;
using System.Linq.Expressions;

namespace ECommerce.Domain.Repositories
{
    public interface IRoleRepository
    {
        Task<bool> AnyAsync(Expression<Func<Role, bool>> spec, CancellationToken cancellationToken);
        Task<long> CountAsync(Expression<Func<Role, bool>> spec, CancellationToken cancellationToken);
        void Delete(Role role);
        Task<Role?> FirstOrDefault(Expression<Func<Role, bool>> spec, CancellationToken cancellationToken);
        Task<IReadOnlyList<Role>> GetList(Expression<Func<Role, bool>> spec, CancellationToken cancellationToken);
        Task InsertAsync(Role role, CancellationToken cancellationToken);
        void Update(Role role);
    }
}
