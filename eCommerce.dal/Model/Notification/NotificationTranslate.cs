using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.dal.Model.Notification
{
    public class NotificationTranslate
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDesc { get; set; }
        public string Description { get; set; }
        public string LanguageCulture { get; set; }
        public int NotificationId { get; set; }
        public Notification Notification { get; set; }
    }
}
