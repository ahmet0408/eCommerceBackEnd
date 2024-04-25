using eCommerce.dal.Model.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.dal.Data.Configuration.ProductConfiguration
{
    public class CategoryTranslateConfiguration: IEntityTypeConfiguration<CategoryTranslate> 
    {
        public void Configure(EntityTypeBuilder<CategoryTranslate> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name);
            builder.Property(p => p.CategoryId);
        }
    }
}
