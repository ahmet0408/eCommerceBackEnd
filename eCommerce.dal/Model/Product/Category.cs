using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.dal.Model.Product
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Icon { get; set; }
        public bool IsPublish { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? ParentId { get; set; }
        public Category ParentCategory { get; set; }
        public int? ParentParentId { get; set; }
        public Category ParentParentCategory { get; set; }
        public ICollection<Category> ChildCategories { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<BrandCategory> BrandCategory { get; set; }
        public ICollection<ProductCategory> ProductCategory { get; set; }
        public ICollection<CategoryTranslate> CategoryTranslates { get; set; }
    }
}
