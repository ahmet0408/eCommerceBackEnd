using eCommerce.dal.Model.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.dal.Data.Configuration.OrderConfiguration
{
    public class OrderItemsConfiguration : IEntityTypeConfiguration<OrderItems>
    {
        public void Configure(EntityTypeBuilder<OrderItems> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Price);
            builder.Property(p => p.Quantity);
            builder.Property(p => p.ProductId);
            builder.Property(p => p.OrderId);
            builder.HasOne(p => p.Order).WithMany(p => p.OrderItems).HasForeignKey(p => p.OrderId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
