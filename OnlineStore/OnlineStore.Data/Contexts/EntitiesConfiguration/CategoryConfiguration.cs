using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Data.Entities;

namespace OnlineStore.Data.Contexts.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Сategory>
    {
        public void Configure(EntityTypeBuilder<Сategory> builder)
        {
            builder
                .HasMany(x => x.Products)
                .WithMany(x => x.Categories);
        }
    }
}
