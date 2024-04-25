using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace eCommerce.Controllers.Admin
{    
    public class AdminController : Controller
    {
        private readonly INotyfService _notyf;
        public AdminController(INotyfService notyf)
        {
            _notyf = notyf;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            _notyf.Custom("You are welcome!", 10, "#B600FF", "fa fa-home");
            return View();
        }
        [HttpGet]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            returnUrl = returnUrl.Replace('!', '&');
            return Redirect(returnUrl);
        }
    }
}
