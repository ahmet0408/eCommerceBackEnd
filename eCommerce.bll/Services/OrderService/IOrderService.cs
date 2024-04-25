using eCommerce.bll.DTOs.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.bll.Services.OrderService
{
    public interface IOrderService
    {
        Task<int> OrderToSave(OrderDTO modelDTO);
        Task<IEnumerable<OrderDTO>> GetAllOrder();
    }
}
