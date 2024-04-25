using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.bll.DTOs.LanguageDTO
{
    public class CreateLanguageDTO
    {
        [Required]
        public string Culture { get; set; }
        [Required]
        public string Name { get; set; }
        public string FlagImage { get; set; }
        [Required]
        public bool IsPublish { get; set; }
        public int DisplayOrder { get; set; }
    }
}
