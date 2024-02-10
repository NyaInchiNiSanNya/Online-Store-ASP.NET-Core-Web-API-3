using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.BusinessLogic.Interfaces;
using OnlineStore.BusinessLogic.Services;
using System.Reflection;

namespace OnlineStore.BusinessLogic.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices
            (this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
