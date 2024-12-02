using DevExtreme.AspNet.Data;
using eCommerce.Binder;
using eCommerce.bll.DTOs.NotificationDTO;
using eCommerce.bll.Services.NotificationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationApiController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationApiController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public async Task<NotificationDTO> GetNotification()
        {
            return await _notificationService.GetNotification();
        } 
        [HttpGet("GetAll")]
        public object GetAllNotification(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<NotificationDTO>(_notificationService.GetAllNotification().OrderBy(o => o.Order).AsQueryable(), loadOptions);

        }
        [HttpDelete("GetAll/{id}")]
        public async Task DeleteCategory(int id)
        {
            await _notificationService.RemoveNotification(id);
        }
    }
}
