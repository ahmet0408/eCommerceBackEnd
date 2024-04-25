using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.bll.DTOs.ProductDTO
{
    public class EditBrandDTO
    {
        public int BrandId { get; set; }
        public ICollection<BrandTranslateDTO> BrandTranslates { get; set; }
        public IFormFile FormIcon { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }
        public bool IsPublish { get; set; }
        public string Categories { get; set; }
        public string CategoryIds { get; set; }
    }
}
