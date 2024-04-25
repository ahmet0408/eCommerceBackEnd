using eCommerce.bll.DTOs.NotificationDTO;
using eCommerce.bll.Services.LanguageService;
using eCommerce.bll.Services.NotificationService;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Controllers.Admin
{
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly ILanguageService _languageService;
        public NotificationController(INotificationService notificationService, ILanguageService languageService)
        {
            _notificationService = notificationService;
            _languageService = languageService;
        }
        public IActionResult Index()
        {
            return View("../Admin/Notification/Index");
        }
        public IActionResult Create()
        {
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View("../Admin/Notification/Create");
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateNotificationDTO createNotificationDTO)
        {
            if (ModelState.IsValid)
            {
                await _notificationService.CreateNotification(createNotificationDTO);
                return RedirectToAction("Index");
            }
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View("../Admin/Notification/Create");
        }
    }
}
