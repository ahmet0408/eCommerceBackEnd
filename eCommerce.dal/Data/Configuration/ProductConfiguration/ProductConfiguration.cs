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
    public class ProductConfiguration: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.SKU);
            builder.Property(p => p.RegularPrice);
            builder.Property(p => p.CreatedAt);
            builder.Property(p => p.DiscountPrice).IsRequired(false).HasDefaultValue(0);
            builder.Property(p => p.IsPublish);
            builder.Property(p => p.IsNew);
            builder.Property(p => p.IsStock);
            builder.Property(p => p.Order);
            builder.Property(p => p.UpdatedAt);
            builder.Property(p => p.BrandId).IsRequired(false);
            builder.HasOne(p => p.Brand).WithMany(p => p.Product).HasForeignKey(p => p.BrandId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
            builder.HasMany(p => p.ProductTranslates).WithOne(p => p.Product).HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
