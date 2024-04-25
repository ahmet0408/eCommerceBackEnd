using System;
using System.Collections.Generic;

namespace eCommerce.dal.Model.Product
{
    public class Product
    {
        public int ProductId { get; set; }
        public double RegularPrice { get; set; }
        public int? DiscountPrice { get; set; }
        public string ImagePath { get; set; }
        public int SKU { get; set; }
        public bool IsPublish { get; set; }
        public bool IsNew { get; set; }
        public bool IsStock { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }
        public ICollection<ProductCategory> ProductCategory { get; set; }
        public ICollection<ProductTranslate> ProductTranslates { get; set; }
    }
}
