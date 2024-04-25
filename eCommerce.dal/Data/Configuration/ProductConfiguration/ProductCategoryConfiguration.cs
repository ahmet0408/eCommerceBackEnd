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
    public class ProductCategoryConfiguration: IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(p => new { p.ProductId, p.CategoryId });
            builder.HasOne(p => p.Category).WithMany(p => p.ProductCategory).HasForeignKey(p => p.CategoryId);
            builder.HasOne(p => p.Product).WithMany(p => p.ProductCategory).HasForeignKey(p => p.ProductId);
        }
    }
}
