using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.bll.DTOs.ProductDTO
{
    public class CreateCategoryDTO
    {
        public ICollection<CategoryTranslateDTO> CategoryTranslates { get; set; }
        public int? ParentId { get; set; }
        public IFormFile FormIcon { get; set; }
        public int Order { get; set; }
        public bool IsPublish { get; set; }
    }
}
