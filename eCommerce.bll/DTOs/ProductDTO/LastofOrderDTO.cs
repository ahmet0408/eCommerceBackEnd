using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.bll.DTOs.ProductDTO
{
    public class LastofOrderDTO
    {
        public int OrderNumber { get; set; }
        public string Phone { get; set; }
        public DateTime DateTime { get; set; } 
    }
}
