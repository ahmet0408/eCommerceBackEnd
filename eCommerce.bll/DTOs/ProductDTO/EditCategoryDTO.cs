using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace eCommerce.bll.DTOs.ProductDTO
{
    public class EditCategoryDTO
    {
        public int CategoryId { get; set; }
        public ICollection<CategoryTranslateDTO> CategoryTranslates { get; set; }
        public int Order { get; set; }
        public IFormFile FormIcon { get; set; }
        public string Icon { get; set; }
        public int? ParentId { get; set; }
        public int? ParentParentId { get; set; }
        public bool IsPublish { get; set; }
    }
}
