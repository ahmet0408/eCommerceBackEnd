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
    public class NotificationTranslateConfiguration: IEntityTypeConfiguration<NotificationTranslate>
    {
        public void Configure(EntityTypeBuilder<NotificationTranslate> builder)
        {
            builder.HasKey(p => p.Id);
        }
    }
}
