using System.Collections.Generic;

namespace eCommerce.dal.Model.Order
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public ICollection<Order> Orders { get; set; } 
    }
}
