using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.dal.Model.Product
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }
        public bool IsPublish { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Product> Product { get; set; }
        public ICollection<BrandCategory> BrandCategory { get; set; }
        public ICollection<BrandTranslate> BrandTranslates { get; set; } 
    }
}
