using eCommerce.bll.DTOs.OrderDTO;
using eCommerce.bll.Services.OrderService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace eCommerce.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]    
    public class OrderApiController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderApiController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> OrderToSave([FromBody] OrderDTO orderDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderService.OrderToSave(orderDTO);
                return Ok(result);
            }
            return Ok("Kabir ýalňyşlyk!");
        }
        [HttpGet("GetAllOrders")]
        public async Task<object> GetAllOrder()
        {
            return await _orderService.GetAllOrder();
        }
    }
}
