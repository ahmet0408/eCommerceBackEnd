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
    public class CategoryConfiguration: IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(p => p.CategoryId);
            builder.Property(p => p.Icon);
            builder.Property(p => p.Order);
            builder.Property(p => p.IsPublish);
            builder.Property(p => p.CreatedAt);
            builder.Property(p => p.UpdatedAt);
            builder.Property(p => p.ParentId).IsRequired(false);
            builder.Property(p => p.ParentParentId).IsRequired(false);
            builder.HasMany(p => p.ChildCategories).WithOne(p => p.ParentCategory).HasForeignKey(p => p.ParentId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.Categories).WithOne(p => p.ParentParentCategory).HasForeignKey(p => p.ParentParentId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.CategoryTranslates).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
