using eCommerce.bll.DTOs.NotificationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.bll.Services.NotificationService
{
    public interface INotificationService
    {
        Task CreateNotification(CreateNotificationDTO modelDTO);
        Task<NotificationDTO> GetNotification();
        Task RemoveNotification(int id);
        IEnumerable<NotificationDTO> GetAllNotification();
    }
}
