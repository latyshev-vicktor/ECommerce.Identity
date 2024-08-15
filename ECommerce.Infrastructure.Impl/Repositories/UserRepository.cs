using CSharpFunctionalExtensions;
using ECommerce.DataAccess.Postgres;
using ECommerce.Domain.DTO.Permission;
using ECommerce.Domain.DTO.User;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Extensions;
using ECommerce.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<IReadOnlyList<UserDto>> GetAllUsersWithPermissions(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .Where(x => !x.IsBlocked)
                .Include(x => x.Roles)
                .ThenInclude(x => x.Permissions)
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    Email = x.Email.Value,
                    UserName = x.UserName,
                    FirstName = x.FullName.FirstName,
                    LastName = x.FullName.LastName,
                    UserType = x.UserType.GetDescription(),
                    Permissions = x.Roles.SelectMany(x => x.Permissions).Select(x => new PermissionDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                    }).ToArray()
                })
                .ToListAsync(cancellationToken);
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

        public async Task<User?> GetUserWithPermissions(Expression<Func<User, bool>> spec, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                            .Include(x => x.Roles)
                                .ThenInclude(x => x.Permissions)
                            .Include(x => x.RefreshTokens)
                            .AsSplitQuery()
                            .FirstOrDefaultAsync(spec, cancellationToken);

            return user;
        }
    }
}
