using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.bll.DTOs.ProductDTO
{
    public class ProductDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public double RegularPrice { get; set; }
        public int DiscountPrice { get; set; }
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }
        public string SubCategory { get; set; }
    }
}
