using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.bll.DTOs.LanguageDTO
{
    public class LanguageDTO
    {
        public string Culture { get; set; }
        public string Name { get; set; }
        public string FlagImage { get; set; }
        public bool IsPublish { get; set; }
        public int DisplayOrder { get; set; }
    }
}
