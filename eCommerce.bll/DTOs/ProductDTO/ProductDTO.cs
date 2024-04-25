using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.bll.DTOs.ProductDTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public double RegularPrice { get; set; }
        public int? DiscountPrice { get; set; }
        public bool IsNew { get; set; }
        public bool IsStock { get; set; }
        public string ImagePath { get; set; }
        public int Order { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int SKU { get; set; }
        public int Quantity { get; set; }
        public string SubCategory { get; set; }
        public string ParentCategory { get; set; }
        public string Categories { get; set; }
    }
}
