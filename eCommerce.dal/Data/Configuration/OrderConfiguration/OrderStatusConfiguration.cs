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
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name);
            builder.Property(p => p.Color);
            builder.HasMany(p => p.Orders).WithOne(p => p.OrderStatus).HasForeignKey(p => p.OrderStatusId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
