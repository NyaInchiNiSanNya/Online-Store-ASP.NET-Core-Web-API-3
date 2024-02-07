using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Data.Contexts;
using OnlineStore.Data.Entities;


namespace OnlineStore.Data.Extensions
{
    public static class DbConfiguration
    {
        public static IServiceCollection AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductsOrdersContext>(opt =>
            {
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                var connString = configuration
                    .GetConnectionString("DefaultConnection");

                opt.UseNpgsql(connString, npgsqlOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "pub");
                    
                });

            });

            services.AddIdentityCore<User>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<ProductsOrdersContext>();

            return services;
        }
    }
}
