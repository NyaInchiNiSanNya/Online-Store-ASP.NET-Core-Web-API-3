using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Data.Entities;

namespace OnlineStore.Data.Contexts.EntitiesConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Сategory>
    {
        public void Configure(EntityTypeBuilder<Сategory> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired();

            builder
                .HasMany(x => x.Products)
                .WithMany(x => x.Categories);
        }
    }
}
