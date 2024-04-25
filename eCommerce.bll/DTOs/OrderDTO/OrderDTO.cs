 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.bll.DTOs.OrderDTO
{
    public class OrderDTO
    {
        public int OrderNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        public int OrderStatusId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime OrderApprovedAt { get; set; }
        public DateTime OrderDeliveredCarrierDate { get; set; }
        [Required]
        public ICollection<OrderItemDTO> Products { get; set; }
    }    
}
