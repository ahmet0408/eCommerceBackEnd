using eCommerce.dal.Model.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.dal.Data.Configuration.NotificationConfiguration
{
    public class NotificationConfiguration: IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.IsPublish);
            builder.HasMany(p => p.NotificationTranslates).WithOne(p => p.Notification).HasForeignKey(p => p.NotificationId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}