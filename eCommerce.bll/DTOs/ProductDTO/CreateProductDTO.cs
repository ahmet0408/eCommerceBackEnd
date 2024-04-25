using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.bll.DTOs.ProductDTO
{
    public class CreateProductDTO
    {
        public ICollection<ProductTranslateDTO> ProductTranslates { get; set; }
        [Required]
        public double RegularPrice { get; set; }
        public int? DiscountPrice { get; set; }
        [Required]
        public IFormFile FormImage { get; set; }
        [Required]
        public int SKU { get; set; }
        public bool IsPublish { get; set; }
        public bool IsNew { get; set; }
        public bool IsStock { get; set; }
        [Required]
        public int Order { get; set; }
        public int? BrandId { get; set; }
        public string CategoryIds { get; set; }
    }
}
