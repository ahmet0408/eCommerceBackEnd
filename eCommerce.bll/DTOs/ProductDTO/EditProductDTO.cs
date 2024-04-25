using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.bll.DTOs.ProductDTO
{
    public class EditProductDTO
    {
        public int ProductId { get; set; }
        public ICollection<ProductTranslateDTO> ProductTranslates { get; set; }
        public double RegularPrice { get; set; }
        public int? DiscountPrice { get; set; }
        public IFormFile FormImage { get; set; }
        public string ImagePath { get; set; }
        public int SKU { get; set; }
        public bool IsPublish { get; set; }
        public bool IsNew { get; set; }
        public bool IsStock { get; set; }
        public int Order { get; set; }
        public int? BrandId { get; set; }
        public string CategoryIds { get; set; }
    }
}
