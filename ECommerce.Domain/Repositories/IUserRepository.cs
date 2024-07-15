using ECommerce.Domain.Entities;
using System.Linq.Expressions;

namespace ECommerce.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<bool> AnyAsync(Expression<Func<User, bool>> spec, CancellationToken cancellationToken);
        Task<long> CountAsync(Expression<Func<User, bool>> spec, CancellationToken cancellationToken);
        void Delete(User user);
        Task<User?> FirstOrDefault(Expression<Func<User, bool>> spec, CancellationToken cancellationToken, params Expression<Func<User, object>>[] includes);
        Task<User?> GetUserWithPermissions(Expression<Func<User, bool>> spec, CancellationToken cancellationToken);
        Task<IReadOnlyList<User>> GetList(Expression<Func<User, bool>> spec, CancellationToken cancellationToken, params Expression<Func<User, object>>[] includes);
        Task InsertAsync(User user, CancellationToken cancellationToken);
        void Update(User user);
    }
}
