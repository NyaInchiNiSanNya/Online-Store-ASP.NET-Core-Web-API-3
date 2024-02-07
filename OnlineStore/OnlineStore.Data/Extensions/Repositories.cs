using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Data.Interfaces;
using OnlineStore.Data.Repositories;
using OnlineStore.Data.Repositories.Implementations;

namespace OnlineStore.Data.Extensions
{
    public static class RepositoriesExtension
    {
        public static IServiceCollection AddRepositories
            (this IServiceCollection repositories)
        {
            repositories.AddScoped<IUnitOfWork, UnitOfWork>();
            repositories.AddScoped<ICategoryRepository, CategoryRepository>();
            repositories.AddScoped<IOrderItemRepository, OrderItemRepository>();
            repositories.AddScoped<IOrderRepository, OrderRepository>();
            repositories.AddScoped<IProductRepository, ProductRepository>();

            return repositories;
        }
    }
}
