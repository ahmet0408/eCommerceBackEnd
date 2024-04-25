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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(p => p.OrderNumber);
            builder.Property(p => p.OrderStatusId);
            builder.Property(p => p.OrderApprovedAt);
            builder.Property(p => p.OrderDeliveredCarrierDate);
            builder.Property(p => p.UserId);
            builder.Property(p => p.CreatedAt);
            builder.Property(p => p.PhoneNumber);
            builder.HasMany(p => p.OrderItems).WithOne(p => p.Order).HasForeignKey(p => p.OrderId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.OrderStatus).WithMany(p => p.Orders).HasForeignKey(p => p.OrderStatusId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.User).WithMany(p => p.Orders).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
