using AutoMapper;
using eCommerce.bll.DTOs.OrderDTO;
using eCommerce.bll.Services.ProductService;
using eCommerce.dal;
using eCommerce.dal.Data;
using eCommerce.dal.Model.Order;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.bll.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public OrderService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, IProductService productService, IMapper mapper)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _productService = productService;
            _mapper = mapper;
        }
        public async Task<int> OrderToSave(OrderDTO modelDTO)
        {
            Order order = new Order();
            if (modelDTO != null)
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(p => p.PhoneNumber == modelDTO.PhoneNumber);
                order.PhoneNumber = modelDTO.PhoneNumber;
                order.UserId = user.Id;
                order.CreatedAt = modelDTO.CreatedAt;
                order.Address = modelDTO.Address;
                order.OrderStatusId = 1;
                order = _dbContext.Order.Add(order).Entity;
                _dbContext.SaveChanges();
                await OrderItemsToSaveForOrder(modelDTO.Products, order.OrderNumber);
                return order.OrderNumber;
            }
            return 0;
        }
        public async Task OrderItemsToSaveForOrder(ICollection<OrderItemDTO> products, int orderId)
        {
            if (products != null)
            {
                foreach (OrderItemDTO orderItemDTO in products)
                {
                    OrderItems orderItems = new OrderItems();
                    var product = await _productService.GetProductWithId(orderItemDTO.ProductId);
                    orderItems.OrderId = orderId;
                    orderItems.ProductId = orderItemDTO.ProductId;
                    orderItems.Price = product.RegularPrice;
                    orderItems.Quantity = orderItemDTO.Quantity;
                    _dbContext.OrderItems.Add(orderItems);
                }
                _dbContext.SaveChanges();
            }
        }
        public async Task<IEnumerable<OrderDTO>> GetAllOrder()
        {
            var order = _dbContext.Order.Include(p => p.OrderItems).Include(p => p.OrderStatus).Include(p => p.User);
            var result = _mapper.Map<IEnumerable<OrderDTO>>(order);
            return result;
        }
    }
}
