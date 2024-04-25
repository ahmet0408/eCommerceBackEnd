using eCommerce.bll.DTOs.ProductDTO;
using eCommerce.bll.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Controllers
{
    public class HomeController : Controller
    {       
        public IActionResult Index()
        {            
            return Redirect("/Identity/Account/Login?ReturnUrl=%2FAdmin");
        }        
    }
}
