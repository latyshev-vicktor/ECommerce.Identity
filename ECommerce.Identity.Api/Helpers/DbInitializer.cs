using ECommerce.Application.Services;
using ECommerce.DataAccess.Postgres;
using ECommerce.Domain.Constants;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Extensions;
using ECommerce.Domain.Repositories;
using ECommerce.Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ECommerce.Api.Helpers
{
    public class DbInitializer(
        IPermissionRepository permissionRepository,
        IRoleRepository roleRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ECommerceIdentityDbContext dbContext)
    {
        private readonly IPermissionRepository _permissionRepository = permissionRepository;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ECommerceIdentityDbContext _dbContext = dbContext;

        public async Task Initializer(CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransaction(cancellationToken);
                var pendingMigration = await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken).ConfigureAwait(false);
                if (pendingMigration.Any())
                    await _dbContext.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);

                await InitPermission(cancellationToken);
                await InitRoles(cancellationToken);   

                await _unitOfWork.CommitTransaction(cancellationToken);
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackTransaction(cancellationToken);
            }
        }

        private async Task InitPermission(CancellationToken cancellationToken)
        {
            foreach(var permission in DefaultPermissions.All)
            {
                var existPermission = await _permissionRepository.FirstOrDefault(PermissionSpecification.ByIds(new[] { permission.Id }), cancellationToken);
                if (existPermission == null)
                {
                    var newPermissionResult = Permission.Create(permission.Id, permission.Name, permission.Description);
                    if (newPermissionResult.IsFailure)
                        throw new InvalidOperationException(newPermissionResult.Error);

                    await _permissionRepository.InsertAsync(newPermissionResult.Value, cancellationToken);
                }
                else
                {
                    if (permission.Name != existPermission.Name || permission.Description != existPermission.Description)
                    {
                        existPermission.SetName(permission.Name);
                        existPermission.SetDescription(permission.Description);
                        existPermission.MakeModify();

                        _permissionRepository.Update(existPermission);
                    }
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task InitRoles(CancellationToken cancellationToken)
        {
            foreach (var role in DefaultRoles.All) 
            {
                var existRole = await _roleRepository.FirstOrDefault(RoleSpecification.ByIds(new[] { role.Id }), cancellationToken, x => x.Permissions);
                if (existRole == null)
                {
                    var newRole = Role.Create(role.Id, role.Name, role.Description);
                    if (newRole.IsFailure)
                        throw new InvalidOperationException(newRole.Error);

                    foreach(var permissionId in role.Permissions)
                        await UpdatePermissionForRoles(newRole.Value, permissionId, cancellationToken);

                    await _roleRepository.InsertAsync(newRole.Value, cancellationToken);
                }
                else
                {
                    foreach(var permissionId in role.Permissions)
                    {
                        if (!existRole.Permissions.Any(x => x.Id == permissionId))
                            await UpdatePermissionForRoles(existRole, permissionId, cancellationToken);
                    }

                    if (existRole.Name != role.Name || existRole.Description != role.Description)
                    {
                        existRole.SetName(role.Name);
                        existRole.SetDescription(role.Description);
                    }

                    _roleRepository.Update(existRole);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task UpdatePermissionForRoles(Role role, Guid permissionId, CancellationToken cancellationToken)
        {
            var addedPermission = await _permissionRepository.FirstOrDefault(PermissionSpecification.ByIds(new[] { permissionId }), cancellationToken);
            if (addedPermission != null)
                role.AddPermission(addedPermission);
        }
    }
}
