using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Contexts.EntitiesConfiguration;
using OnlineStore.Data.Entities;

namespace OnlineStore.Data.Contexts
{
    public class ProductsOrdersContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Сategory> Categories { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public ProductsOrdersContext(DbContextOptions<ProductsOrdersContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            base.OnConfiguring(optionBuilder);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("pub");

        }
    }
}
