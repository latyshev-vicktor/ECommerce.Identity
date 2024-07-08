using ECommerce.DataAccess.Postgres;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Infrastructure.Impl.Repositories
{
    public class PermissionRepository(ECommerceIdentityDbContext dbContext) : IPermissionRepository
    {
        private readonly ECommerceIdentityDbContext _dbContext = dbContext;

        public async Task<bool> AnyAsync(Expression<Func<Permission, bool>> spec, CancellationToken cancellationToken)
            => await _dbContext.Permissions.AnyAsync(spec, cancellationToken);

        public async Task<long> CountAsync(Expression<Func<Permission, bool>> spec, CancellationToken cancellationToken)
            => await _dbContext.Permissions.CountAsync(spec, cancellationToken);

        public void Delete(Permission permission)
        {
            _dbContext.Permissions.Remove(permission);
        }

        public async Task<Permission?> FirstOrDefault(Expression<Func<Permission, bool>> spec, CancellationToken cancellationToken, params Expression<Func<Permission, object>>[] includes)
        {
            var dbQuery = _dbContext.Permissions.Where(spec);

            if (includes != null)
                dbQuery = includes.Aggregate(dbQuery, (current, include) => current.Include(include));

            return await dbQuery.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Permission>> GetList(Expression<Func<Permission, bool>> spec, CancellationToken cancellationToken, params Expression<Func<Permission, object>>[] includes)
        {
            var dbQuery = _dbContext.Permissions.Where(spec);

            if (includes != null)
                dbQuery = includes.Aggregate(dbQuery, (current, include) => current.Include(include));

            return await dbQuery.ToListAsync(cancellationToken);
        }

        public async Task InsertAsync(Permission permission, CancellationToken cancellationToken)
        {
            await _dbContext.Permissions.AddAsync(permission, cancellationToken);
        }

        public void Update(Permission permission)
        {
            _dbContext.Permissions.Update(permission);
        }
    }
}
