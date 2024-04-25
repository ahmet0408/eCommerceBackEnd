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
    public class BrandTranslateConfiguration : IEntityTypeConfiguration<BrandTranslate>
    {
        public void Configure(EntityTypeBuilder<BrandTranslate> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name);
            builder.Property(p => p.BrandId);
        } 
    }
}
