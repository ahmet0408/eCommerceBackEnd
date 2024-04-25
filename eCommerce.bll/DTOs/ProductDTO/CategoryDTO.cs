using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.bll.DTOs.ProductDTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }
        public int? ParentId { get; set; }
        public int? ParentParentId { get; set; }
        public string ParentParentCategory { get; set; }
        public string ParentCategory { get; set; }
    }
}
