using eCommerce.dal.Model.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.dal.Data.Configuration.ProductConfiguration
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(p => p.BrandId);
            builder.Property(p => p.Icon);
            builder.Property(p => p.CreatedAt);
            builder.Property(p => p.UpdatedAt);
            builder.HasMany(p => p.Product).WithOne(p => p.Brand).HasForeignKey(p => p.BrandId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.BrandTranslates).WithOne(p => p.Brand).HasForeignKey(p => p.BrandId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
