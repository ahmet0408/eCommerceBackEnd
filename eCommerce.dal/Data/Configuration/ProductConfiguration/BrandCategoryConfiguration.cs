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
    public class BrandCategoryConfiguration : IEntityTypeConfiguration<BrandCategory>
    {
        public void Configure(EntityTypeBuilder<BrandCategory> builder)
        {
            builder.HasKey(p => new { p.BrandId, p.CategoryId });
            builder.Property(p => p.ParentId);
            builder.HasOne(p => p.Brand).WithMany(p => p.BrandCategory).HasForeignKey(p => p.BrandId);
            builder.HasOne(p => p.Category).WithMany(p => p.BrandCategory).HasForeignKey(p => p.CategoryId);
        }
    }
}
