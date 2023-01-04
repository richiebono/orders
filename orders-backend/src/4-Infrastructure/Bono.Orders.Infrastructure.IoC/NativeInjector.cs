using Microsoft.Extensions.DependencyInjection;
using System;
using Bono.Orders.Application.Interfaces;
using Bono.Orders.Application.Services;
using Bono.Orders.Data.Repositories;
using Bono.Orders.Domain.Interfaces.Repository;
using Bono.Orders.Domain.Validations;
using Bono.Orders.Infrastructure.Utils;

namespace Bono.Orders.Infrastructure.IoC
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Services

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderTypeService, OrderTypeService>();

            #endregion

            #region Domain

            services.AddScoped<ValidationResult, ValidationResult>();

            #endregion 

            #region Repositories

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderTypeRepository, OrderTypeRepository>();

            #endregion

            #region Utils
                        
            services.AddScoped<Settings, Settings>();
            services.AddScoped<Security, Security>();
            

            #endregion 
        }
    }
}
