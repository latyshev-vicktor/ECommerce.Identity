using ECommerce.DataAccess.Postgres;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Impl.Repositories
{
    public class RoleRepository(ECommerceIdentityDbContext dbContext) : IRoleRepository
    {
        private readonly ECommerceIdentityDbContext _dbContext = dbContext;

        public async Task<bool> AnyAsync(Expression<Func<Role, bool>> spec, CancellationToken cancellationToken)
            => await _dbContext.Roles.AnyAsync(spec, cancellationToken);

        public async Task<long> CountAsync(Expression<Func<Role, bool>> spec, CancellationToken cancellationToken)
            => await _dbContext.Roles.CountAsync(spec, cancellationToken);

        public void Delete(Role role)
        {
            _dbContext.Roles.Remove(role);
        }

        public async Task<Role?> FirstOrDefault(Expression<Func<Role, bool>> spec, CancellationToken cancellationToken, params Expression<Func<Role, object>>[] includes)
        {
            var dbQuery = _dbContext.Roles.Where(spec);
            if (includes != null)
                dbQuery = includes.Aggregate(dbQuery, (current, include) => current.Include(include));

            return await dbQuery.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Role>> GetList(Expression<Func<Role, bool>> spec, CancellationToken cancellationToken, params Expression<Func<Role, object>>[] includes)
        {
            var dbQuery = _dbContext.Roles.Where(spec);
            if (includes != null)
                dbQuery = includes.Aggregate(dbQuery, (current, include) => current.Include(include));

            return await dbQuery.ToListAsync(cancellationToken);
        }

        public async Task InsertAsync(Role role, CancellationToken cancellationToken)
        {
            await _dbContext.Roles.AddAsync(role, cancellationToken);
        }

        public void Update(Role role)
        {
            _dbContext.Roles.Update(role);
        }
    }
}
