using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.dal.Model.Product
{
    public class BrandCategory
    {
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int CategoryId { get; set; }
        public int ParentId { get; set; } 
        public Category Category { get; set; }
    }
}
