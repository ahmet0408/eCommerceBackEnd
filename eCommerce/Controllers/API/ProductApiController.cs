using DevExtreme.AspNet.Data;
using eCommerce.Binder;
using eCommerce.bll.DTOs.ProductDTO;
using eCommerce.bll.Services.ProductService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductApiController(IProductService productService)
        {
            _productService = productService;
        }
        //Category
        [HttpGet]
        public async Task<object> GetAllCategory(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<CategoryDTO>(await _productService.GetAllCategory(), loadOptions);
        }
        [HttpDelete("{id}")]
        public async Task DeleteCategory(int id)
        {
            await _productService.RemoveCategory(id);
        }
        [HttpGet("GetAllParent")]
        public object GetAllParent(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<CategoryDTO>(_productService.GetAllCategoryWithoutParent().OrderBy(o => o.Order).AsQueryable(), loadOptions);
        }
        [HttpGet("SubCategory/{id}")]
        public object GetAllSubCategory(int id, DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<CategoryDTO>(_productService.GetAllSubCategoryWithParentId(id).AsQueryable(), loadOptions);
        }
        [HttpGet("ChildCategory/{id}")]
        public object GetAllChildCategoryWithParent(int id, DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<CategoryDTO>(_productService.GetChildCategoryWithSubCategoryId(id).AsQueryable(), loadOptions);
        }        
        [HttpGet("GetAllCategoryWithParentId/{id}")]
        public object GetAllCategoryWithParentId(int id, DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<CategoryDTO>(_productService.GetAllCategoryWithParentId(id).AsQueryable(), loadOptions);
        }
        [HttpGet("GetCategoryWithId/{id}")]
        public async Task<CategoryDTO> GetCategoryWithid(int id)
        {
            return await _productService.GetCategoryWithId(id);
        }
        //Brand
        [HttpGet("GetAllBrandWithCategory/{id}")]
        public object GetAllBrandWithCategory(int id, DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<BrandDTO>(_productService.GetAllBrandWithCategoryId(id).AsQueryable(), loadOptions);
        }
        [HttpGet("GetBrandWithId/{id}")]
        public async Task<BrandDTO> GetAllBrandWithId(int id, DataSourceLoadOptions loadOptions)
        {
            return await _productService.GetBrandWithId(id);
        }
        [HttpGet("GetAllBrand")]
        public object GetAllBrand(DataSourceLoadOptions loadOptions)
        {            
            return DataSourceLoader.Load<BrandDTO>(_productService.GetAllBrand().AsQueryable(), loadOptions);
        }
        [HttpDelete("GetAllBrand/{id}")]
        public async Task DeleteBrand(int id)
        {
            await _productService.RemoveBrand(id);
        }        
        //Product        
        [HttpGet("GetAllProductWithCategoryId/{id}")]
        public async Task<object> GetAllProductWithCategoryId(int id, DataSourceLoadOptions loadOptions)
        {
            var product = await _productService.GetAllProductWithCategoryId(id);
            return DataSourceLoader.Load<ProductDTO>(product.AsQueryable(), loadOptions);
        }
        [HttpGet("GetAllProductWithBrandId/{id}")]
        public async Task<object> GetAllProductWithBrandId(int id, int brand, DataSourceLoadOptions loadOptions)
        {
            var product = await _productService.GetAllProductWithBrandId(id, brand);
            return DataSourceLoader.Load<ProductDTO>(product.AsQueryable(), loadOptions);
        }
        [HttpGet("GetProductWithId/{id}")]
        public async Task<ProductDetailDTO> GetProductWithId(int id, DataSourceLoadOptions loadOptions)
        {
            return await _productService.GetProductWithId(id);
        }
        [HttpGet("GetAllDiscountProduct")]
        public async Task<object> GetAllDiscountProduct(DataSourceLoadOptions loadOptions)
        {
            var product = await _productService.GetAllDiscountProduct();
            return DataSourceLoader.Load<ProductDTO>(product.OrderByDescending(o => o.DiscountPrice).AsQueryable(), loadOptions);
        }
        [HttpGet("GetAllStockProduct")]
        public async Task<object> GetAllStockProduct(DataSourceLoadOptions loadOptions)
        {
            var product = await _productService.GetAllStockProduct();
            return DataSourceLoader.Load<ProductDTO>(product.AsQueryable(), loadOptions);
        }
        [HttpGet("GetAllNewProduct")]
        public async Task<object> GetAllNewProduct(DataSourceLoadOptions loadOptions)
        {
            var product =await _productService.GetAllNewProduct();
            return DataSourceLoader.Load<ProductDTO>(product.AsQueryable(), loadOptions);
        }
        [HttpDelete("GetAllProductWithCategory/{id}")]
        public async Task RemoveProduct(int id)
        {
            await _productService.RemoveProduct(id);
        }        
    }
}
