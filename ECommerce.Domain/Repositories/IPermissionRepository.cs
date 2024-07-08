using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Repositories
{
    public interface IPermissionRepository
    {
        Task<bool> AnyAsync(Expression<Func<Permission, bool>> spec, CancellationToken cancellationToken);
        Task<long> CountAsync(Expression<Func<Permission, bool>> spec, CancellationToken cancellationToken);
        void Delete(Permission permission);
        Task<Permission?> FirstOrDefault(Expression<Func<Permission, bool>> spec, CancellationToken cancellationToken, params Expression<Func<Permission, object>>[] includes);
        Task<IReadOnlyList<Permission>> GetList(Expression<Func<Permission, bool>> spec, CancellationToken cancellationToken, params Expression<Func<Permission, object>>[] includes);
        Task InsertAsync(Permission permission, CancellationToken cancellationToken);
        void Update(Permission permission);
    }
}
