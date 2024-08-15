using ECommerce.Application.Services;
using ECommerce.Domain.Repositories;
using ECommerce.Infrastructure.Impl.Repositories;
using ECommerce.Infrastructure.Impl.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Impl
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped<IJwtProvider, JwtProvider>();

            return services;
        }
    }
}
