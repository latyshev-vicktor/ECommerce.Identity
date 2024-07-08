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
    public class UserRepository(ECommerceIdentityDbContext dbContext) : IUserRepository
    {
        private readonly ECommerceIdentityDbContext _dbContext = dbContext;

        public async Task<bool> AnyAsync(Expression<Func<User, bool>> spec, CancellationToken cancellationToken)
            => await _dbContext.Users.AnyAsync(spec, cancellationToken);

        public async Task<long> CountAsync(Expression<Func<User, bool>> spec, CancellationToken cancellationToken)
            => await _dbContext.Users.CountAsync(spec, cancellationToken);

        public async Task<User?> FirstOrDefault(Expression<Func<User, bool>> spec, CancellationToken cancellationToken, params Expression<Func<User, object>>[] includes)
        {
            var dbQuery = _dbContext.Users.Where(spec);
            if (includes != null)
            {
                dbQuery = includes.Aggregate(dbQuery, (current, include) => current.Include(include));
            }

            return await dbQuery.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<User>> GetList(Expression<Func<User, bool>> spec, CancellationToken cancellationToken, params Expression<Func<User, object>>[] includes)
        {
            var dbQuery = _dbContext.Users.Where(spec);
            if (includes != null)
            {
                dbQuery = includes.Aggregate(dbQuery, (current, include) => current.Include(include));
            }

            return await dbQuery.ToListAsync(cancellationToken);
        }

        public async Task InsertAsync(User user, CancellationToken cancellationToken)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
        }

        public void Update(User user)
        {
            user.MakeModify();
            _dbContext.Users.Update(user);
        }

        public void Delete(User user)
        {
            _dbContext.Users.Remove(user);
        }

        public async Task<HashSet<Permission>> GetAllPermissionsByUserId(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                                 .AsNoTracking()
                                 .Where(x => !x.IsBlocked && x.Id == userId)
                                 .Include(x => x.Roles)
                                 .ThenInclude(x => x.Permissions)
                                 .AsSplitQuery()
                                 .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
                throw new ArgumentNullException("Не найден пользователь по переданному идентификатору");

            return user.Roles.SelectMany(x => x.Permissions).ToHashSet();
        }
    }
}
