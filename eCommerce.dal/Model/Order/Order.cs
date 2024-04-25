using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.dal.Model.Order
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public ICollection<OrderItems> OrderItems { get; set; }        
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime OrderApprovedAt { get; set; }
        public DateTime OrderDeliveredCarrierDate { get; set; }        
    }
}
