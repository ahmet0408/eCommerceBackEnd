using eCommerce.bll.Services.UserService;
using eCommerce.bll.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace eCommerce.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> RegisterAsync([FromForm] RegisterModel model)        
        {  
            await _userService.RegisterAsync(model);
            return Redirect("http://192.168.72.1:3000/auth/login");
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {            
            var result = await _userService.GetTokenAsync(model);
            return Ok(result);
        }                
    }
}
