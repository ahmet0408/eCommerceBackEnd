using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.dal.Model.Notification
{
    public class Notification
    {
        public int Id { get; set; }        
        public int Order { get; set; }
        public bool IsPublish { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<NotificationTranslate> NotificationTranslates { get; set; }
    }
}
