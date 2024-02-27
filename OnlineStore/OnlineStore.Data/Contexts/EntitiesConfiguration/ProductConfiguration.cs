using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Entities;

namespace OnlineStore.Data.Contexts.EntitiesConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Сategory>
    {
        public void Configure(EntityTypeBuilder<Сategory> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired();
        }
    }
}
