using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.bll.DTOs.NotificationDTO
{
    public class CreateNotificationDTO
    {
        public ICollection<NotificationTranslateDTO> NotificationTranslates { get; set; }
        public int Order { get; set; }
        public string Type { get; set; }
        public IFormFile FormImage { get; set; }
        public bool IsPublish { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
