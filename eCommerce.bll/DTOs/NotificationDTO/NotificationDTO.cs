using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.bll.DTOs.NotificationDTO
{
    public class NotificationDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDesc { get; set; }
        public string Image { get; set; }
        public int Order { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
