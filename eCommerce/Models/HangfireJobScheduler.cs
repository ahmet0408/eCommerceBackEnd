using eCommerce.bll.Services.OrderService;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using TSTB.BLL.Services.CurrencyRateService;

namespace eCommerce.Models
{
    public class HangfireJobScheduler
    {
        public static void ScheduleRecurringJobs()
        {
            //RecurringJob.RemoveIfExists(nameof(OrderService));
            //RecurringJob.AddOrUpdate<OrderService>(nameof(OrderService),
            //    job => job.Run(JobCancellationToken.Null), Cron.Weekly(DayOfWeek.Monday), TimeZoneInfo.Local);
        }
    }
}
