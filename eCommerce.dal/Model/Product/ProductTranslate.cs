using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.dal.Model.Product
{
    public class ProductTranslate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public string LanguageCulture { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
