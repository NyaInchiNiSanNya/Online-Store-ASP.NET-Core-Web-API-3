using Microsoft.Extensions.DependencyInjection;
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
