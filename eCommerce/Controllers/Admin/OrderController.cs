using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers.Admin
{
    public class OrderController : Controller
    {
        public IActionResult Order()
        {
            return View("../Admin/Order/Order");
        }
    }
}
