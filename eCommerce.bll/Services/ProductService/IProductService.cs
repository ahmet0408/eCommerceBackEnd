using eCommerce.bll.DTOs.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.bll.Services.ProductService
{
    public interface IProductService
    {
        //Category
        Task CreateCategory(CreateCategoryDTO modelDTO);
        Task<EditCategoryDTO> GetCategoryForEditById(int id);
        Task EditCategory(EditCategoryDTO modelDTO);
        Task RemoveCategory(int id);
        IEnumerable<CategoryDTO> GetAllCategoryWithoutParent();
        IEnumerable<CategoryDTO> GetAllSubCategoryWithParentId(int id);
        Task<IEnumerable<CategoryDTO>> GetAllCategory();
        Task<CategoryDTO> GetCategoryWithId(int id);
        IEnumerable<CategoryDTO> GetChildCategoryWithSubCategoryId(int id);
        IEnumerable<CategoryDTO> GetAllCategoryWithParentId(int id);
        //Brand
        Task CreateBrand(CreateBrandDTO modelDTO);
        Task<EditBrandDTO> GetBrandForEditById(int id);
        Task EditBrand(EditBrandDTO modelDTO);
        Task RemoveBrand(int id);
        IEnumerable<BrandDTO> GetAllBrandWithCategoryId(int id);
        IEnumerable<BrandDTO> GetAllBrand();
        Task<BrandDTO> GetBrandWithId(int id);
        //Product
        Task CreateProduct(CreateProductDTO modelDTO);
        Task<EditProductDTO> GetProductForEditById(int id);
        Task EditProduct(EditProductDTO modelDTO);
        Task RemoveProduct(int id);
        Task CreateProductCategory(int id, List<int> categoryId);
        Task<IEnumerable<ProductDTO>> GetAllProductWithCategoryId(int id);
        Task<IEnumerable<ProductDTO>> GetAllProductWithBrandId(int id, int brandId);
        Task<IEnumerable<ProductDTO>> GetAllDiscountProduct();
        Task<IEnumerable<ProductDTO>> GetAllStockProduct();
        Task<IEnumerable<ProductDTO>> GetAllNewProduct();
        Task<ProductDetailDTO> GetProductWithId(int id);
        IEnumerable<ProductDTO> GetProductWithSearch(string sText);
    }
}
