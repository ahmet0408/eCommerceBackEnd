using AutoMapper;
using eCommerce.bll.DTOs.ProductDTO;
using eCommerce.bll.Services.ImageService;
using eCommerce.dal.Data;
using eCommerce.dal.Model.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.bll.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        public ProductService(ApplicationDbContext dbContext, IMapper mapper, IImageService imageService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _imageService = imageService;
        }
        //Category
        public async Task CreateCategory(CreateCategoryDTO modelDTO)
        {
            if (modelDTO != null)
            {
                Category category = _mapper.Map<Category>(modelDTO);
                if (modelDTO.FormIcon != null)
                {
                    try
                    {
                        category.Icon = await _imageService.UploadImage(modelDTO.FormIcon, "Product/Category");
                    }
                    catch (Exception e)
                    {
                        using (StreamWriter file = new StreamWriter("error.txt", true))
                        {
                            file.WriteLine(e.Message);
                        }
                    }
                }
                if (modelDTO.ParentId != null)
                {
                    var parentId = (int)modelDTO.ParentId;
                    var parentCategory = await GetCategoryWithId(parentId);
                    if (parentCategory.ParentId == null && parentCategory.ParentParentId == null)
                    {
                        category.ParentId = null;
                        category.ParentParentId = (int)modelDTO.ParentId;
                    }
                    else
                    {
                        category.ParentId = (int)modelDTO.ParentId;
                        category.ParentParentId = parentCategory.ParentParentId;
                    }
                }
                await _dbContext.Category.AddAsync(category);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<EditCategoryDTO> GetCategoryForEditById(int id)
        {
            var category = await _dbContext.Category.Include(p => p.CategoryTranslates).SingleOrDefaultAsync(p => p.CategoryId == id);
            var result = _mapper.Map<EditCategoryDTO>(category);
            if (result.ParentId == null && result.ParentParentId == null) { result.ParentId = 0; } else if (result.ParentId == null) { result.ParentId = result.ParentParentId; }
            return result;
        }
        public async Task EditCategory(EditCategoryDTO modelDTO)
        {
            Category category = _mapper.Map<Category>(modelDTO);
            if (modelDTO != null)
            {
                if (modelDTO.FormIcon != null)
                {
                    _imageService.DeleteImage(modelDTO.Icon, "Product/Category");
                    category.Icon = await _imageService.UploadImage(modelDTO.FormIcon, "Product/Category");
                }
                else
                {
                    category.Icon = modelDTO.Icon;
                }
                if (modelDTO.ParentId != 0)
                {
                    var parentId = (int)modelDTO.ParentId;
                    var parentCategory = await GetCategoryWithId(parentId);
                    if (parentCategory.ParentId == null && parentCategory.ParentParentId == null)
                    {
                        category.ParentId = null;
                        category.ParentParentId = (int)modelDTO.ParentId;
                    }
                    else
                    {
                        category.ParentId = (int)modelDTO.ParentId;
                        category.ParentParentId = parentCategory.ParentParentId;
                    }
                }
                else
                {
                    category.ParentParentId = null;
                    category.ParentId = null;
                }
                _dbContext.Category.Update(category);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task RemoveCategory(int id)
        {
            Category category = await _dbContext.Category.FindAsync(id);
            if (!string.IsNullOrEmpty(category.Icon))
            {
                _imageService.DeleteImage(category.Icon, "Product/Category");
            }
            _dbContext.Category.Remove(category);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<CategoryDTO>> GetAllCategory()
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var category = _dbContext.Category.Where(p => p.IsPublish == true).Include(p => p.CategoryTranslates.Where(p => p.LanguageCulture == culture));
            var result = _mapper.Map<IEnumerable<CategoryDTO>>(category);
            foreach (CategoryDTO categoryDTO in result)
            {
                if (categoryDTO.ParentId != null)
                {
                    var parentCategory = await GetCategoryWithId((int)categoryDTO.ParentId);
                    categoryDTO.ParentCategory = parentCategory.Name;
                }
                else if (categoryDTO.ParentParentId != null)
                {
                    var parentCategory = await GetCategoryWithId((int)categoryDTO.ParentParentId);
                    categoryDTO.ParentCategory = parentCategory.Name;
                }
                else
                {
                    categoryDTO.ParentCategory = "";
                }
            }
            return result;
        }
        public IEnumerable<CategoryDTO> GetAllCategoryWithoutParent()
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var category = _dbContext.Category.Where(p => p.IsPublish == true && p.ParentId == null && p.ParentParentId == null).Include(p => p.CategoryTranslates.Where(p => p.LanguageCulture == culture));
            var result = _mapper.Map<IEnumerable<CategoryDTO>>(category);
            return result;
        }
        public IEnumerable<CategoryDTO> GetAllSubCategoryWithParentId(int id)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var category = _dbContext.Category.Where(p => p.IsPublish == true && p.ParentId == null && p.ParentParentId == id).Include(p => p.CategoryTranslates.Where(p => p.LanguageCulture == culture));
            var result = _mapper.Map<IEnumerable<CategoryDTO>>(category);
            return result;
        }
        public IEnumerable<CategoryDTO> GetChildCategoryWithSubCategoryId(int id)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var category = _dbContext.Category.Where(p => p.IsPublish == true && p.ParentId != null && p.ParentParentId == id).Include(p => p.CategoryTranslates.Where(p => p.LanguageCulture == culture));
            var result = _mapper.Map<IEnumerable<CategoryDTO>>(category);
            return result;
        }
        public IEnumerable<CategoryDTO> GetAllCategoryWithParentId(int id)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var category = _dbContext.Category.Where(p => p.IsPublish == true && p.ParentId == id).Include(p => p.CategoryTranslates);
            var result = _mapper.Map<IEnumerable<CategoryDTO>>(category);
            return result;
        }
        public async Task<CategoryDTO> GetCategoryWithId(int id)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var category = await _dbContext.Category.Where(p => p.IsPublish == true).Include(p => p.CategoryTranslates.Where(p => p.LanguageCulture == culture)).SingleOrDefaultAsync(p => p.CategoryId == id);
            var result = _mapper.Map<CategoryDTO>(category);
            if (result.ParentParentId != null)
            {
                var parentCategory = await _dbContext.Category.Where(p => p.IsPublish == true).Include(p => p.CategoryTranslates.Where(p => p.LanguageCulture == culture)).SingleOrDefaultAsync(p => p.CategoryId == result.ParentParentId);
                result.ParentParentCategory = parentCategory.CategoryTranslates.Select(p => p.Name).FirstOrDefault();
            }
            else result.ParentParentCategory = result.Name;
            if (result.ParentId != null)
            {
                var parentCategoryy = await _dbContext.Category.Where(p => p.IsPublish == true).Include(p => p.CategoryTranslates.Where(p => p.LanguageCulture == culture)).SingleOrDefaultAsync(p => p.CategoryId == result.ParentId);
                result.ParentCategory = parentCategoryy.CategoryTranslates.Select(p => p.Name).FirstOrDefault();
            }
            return result;
        }
        //Brand
        public async Task CreateBrandCategory(int id, List<int> categoryId)
        {
            foreach (var cId in categoryId)
            {
                BrandCategory brandCategory = new BrandCategory();
                brandCategory.BrandId = id;
                brandCategory.CategoryId = cId;
                var parentCategory = await GetCategoryWithId(cId);
                brandCategory.ParentId = (int)parentCategory.ParentParentId;
                await _dbContext.BrandCategory.AddAsync(brandCategory);
            }
            await _dbContext.SaveChangesAsync();
        }
        public async Task CreateBrand(CreateBrandDTO modelDTO)
        {
            if (modelDTO != null)
            {
                Brand brand = _mapper.Map<Brand>(modelDTO);
                if (modelDTO.FormIcon != null)
                {
                    try
                    {
                        brand.Icon = await _imageService.UploadImage(modelDTO.FormIcon, "Product/Brand");
                    }
                    catch (Exception e)
                    {
                        using (StreamWriter file = new StreamWriter("error.txt", true))
                        {
                            file.WriteLine(e.Message);
                        }
                    }
                }
                List<int> categoryIds = new List<int>();
                if (!string.IsNullOrEmpty(modelDTO.CategoryIds))
                {
                    var arIds = modelDTO.CategoryIds.Split(",");
                    foreach (var arId in arIds)
                    {
                        try
                        {
                            categoryIds.Add(int.Parse(arId));
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                brand = _dbContext.Brand.Add(brand).Entity;
                _dbContext.SaveChanges();
                await CreateBrandCategory(brand.BrandId, categoryIds);
            }
        }
        public async Task<EditBrandDTO> GetBrandForEditById(int id)
        {
            List<BrandCategory> brands = await _dbContext.BrandCategory.Include(p => p.Brand).Include(p => p.Brand.BrandTranslates).Where(p => p.BrandId == id).ToListAsync();
            string category = "";
            foreach (BrandCategory brand in brands)
            {
                category = category + brand.CategoryId + ',';
            }
            var aBrand = brands[0].Brand;
            var result = _mapper.Map<EditBrandDTO>(aBrand);
            result.Categories = category;
            return result;
        }
        public async Task EditBrand(EditBrandDTO modelDTO)
        {
            if (modelDTO != null)
            {
                Brand brand = _mapper.Map<Brand>(modelDTO);
                if (modelDTO.FormIcon != null)
                {
                    _imageService.DeleteImage(modelDTO.Icon, "Product/Brand");
                    brand.Icon = await _imageService.UploadImage(modelDTO.FormIcon, "Product/Brand");
                }
                else
                {
                    brand.Icon = modelDTO.Icon;
                }
                var remBrCt = _dbContext.BrandCategory.Where(p => p.BrandId == brand.BrandId).ToList();
                _dbContext.BrandCategory.RemoveRange(remBrCt);
                List<int> categoryIds = new List<int>();
                if (!string.IsNullOrEmpty(modelDTO.CategoryIds))
                {
                    var arIds = modelDTO.CategoryIds.Split(",");
                    foreach (var arId in arIds)
                    {
                        try
                        {
                            categoryIds.Add(int.Parse(arId));
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                brand = _dbContext.Brand.Update(brand).Entity;
                _dbContext.SaveChanges();
                await CreateBrandCategory(brand.BrandId, categoryIds);
            }

        }
        public async Task RemoveBrand(int id)
        {
            Brand brand = await _dbContext.Brand.FindAsync(id);
            if (!string.IsNullOrEmpty(brand.Icon))
            {
                _imageService.DeleteImage(brand.Icon, "Product/Brand");
            }
            _dbContext.Brand.Remove(brand);
            await _dbContext.SaveChangesAsync();
        }
        public IEnumerable<BrandDTO> GetAllBrandWithCategoryId(int id)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var brand = _dbContext.BrandCategory.Include(p => p.Category).Where(p => p.CategoryId == id || p.Category.ParentId == id || p.ParentId == id)
                .Include(p => p.Brand)
                            .ThenInclude(p => p.BrandTranslates.Where(p => p.LanguageCulture == culture));
            var result = _mapper.Map<IEnumerable<BrandDTO>>(brand);
            return result;
        }
        public IEnumerable<BrandDTO> GetAllBrand()
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var brand = _dbContext.BrandCategory.Include(p => p.Brand)
                .ThenInclude(p => p.BrandTranslates.Where(p => p.LanguageCulture == culture))
                .Include(p => p.Category)
                    .ThenInclude(p => p.CategoryTranslates);
            var result = _mapper.Map<IEnumerable<BrandDTO>>(brand);
            return result;
        }
        public async Task<BrandDTO> GetBrandWithId(int id)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var brand = await _dbContext.Brand.Where(p => p.IsPublish == true).Include(p => p.BrandTranslates.Where(p => p.LanguageCulture == culture)).SingleOrDefaultAsync(p => p.BrandId == id);
            var result = _mapper.Map<BrandDTO>(brand);
            return result;
        }
        //Product
        public async Task CreateProductCategory(int id, List<int> categoryId)
        {
            foreach (var cId in categoryId)
            {
                ProductCategory productCategory = new ProductCategory();
                productCategory.ProductId = id;
                productCategory.CategoryId = cId;
                await _dbContext.ProductCategory.AddAsync(productCategory);
            }
            await _dbContext.SaveChangesAsync();
        }
        public async Task CreateProduct(CreateProductDTO modelDTO)
        {
            if (modelDTO != null)
            {
                Product product = _mapper.Map<Product>(modelDTO);
                if (modelDTO.FormImage != null)
                {
                    try
                    {
                        product.ImagePath = await _imageService.UploadImage(modelDTO.FormImage, "Product/Product");
                    }
                    catch (Exception e)
                    {
                        using (StreamWriter file = new StreamWriter("error.txt", true))
                        {
                            file.WriteLine(e.Message);
                        }
                    }
                }
                List<int> categoryIds = new List<int>();
                if (!string.IsNullOrEmpty(modelDTO.CategoryIds))
                {
                    var arIds = modelDTO.CategoryIds.Split(",");
                    foreach (var arId in arIds)
                    {
                        try
                        {
                            categoryIds.Add(int.Parse(arId));
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                product = _dbContext.Product.Add(product).Entity;
                _dbContext.SaveChanges();
                await CreateProductCategory(product.ProductId, categoryIds);
            }
        }
        public async Task<EditProductDTO> GetProductForEditById(int id)
        {
            var product = await _dbContext.ProductCategory.Where(p => p.ProductId == id).Include(p => p.Product).ThenInclude(p => p.ProductTranslates).Include(p => p.Category).ToListAsync();
            var categories = "";
            foreach (ProductCategory product1 in product)
            {
                categories = categories + product1.CategoryId + ",";
            }
            EditProductDTO editProductDTO = _mapper.Map<EditProductDTO>(product[0].Product);
            editProductDTO.CategoryIds = categories;
            return editProductDTO;
        }
        public async Task EditProduct(EditProductDTO modelDTO)
        {
            if (modelDTO != null)
            {
                Product product = _mapper.Map<Product>(modelDTO);
                if (modelDTO.FormImage != null)
                {
                    _imageService.DeleteImage(modelDTO.ImagePath, "Product/Product");
                    product.ImagePath = await _imageService.UploadImage(modelDTO.FormImage, "Product/Product");
                }
                else
                {
                    product.ImagePath = modelDTO.ImagePath;
                }
                var remPrCt = _dbContext.ProductCategory.Where(p => p.ProductId == product.ProductId).ToList();
                _dbContext.ProductCategory.RemoveRange(remPrCt);
                List<int> categoryIds = new List<int>();
                if (!string.IsNullOrEmpty(modelDTO.CategoryIds))
                {
                    var arIds = modelDTO.CategoryIds.Split(",");
                    foreach (var arId in arIds)
                    {
                        try
                        {
                            categoryIds.Add(int.Parse(arId));
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                product = _dbContext.Product.Update(product).Entity;
                _dbContext.SaveChanges();
                await CreateProductCategory(product.ProductId, categoryIds);
            }
        }
        public async Task RemoveProduct(int id)
        {
            var product = await _dbContext.Product.FindAsync(id);

            if (!string.IsNullOrEmpty(product.ImagePath))
            {
                _imageService.DeleteImage(product.ImagePath, "Product/Product");
            }
            _dbContext.Product.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<ProductDTO>> GetAllDiscountProduct()
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var product = _dbContext.Product.Include(p => p.ProductCategory).Include(p => p.ProductTranslates.Where(p => p.LanguageCulture == culture)).Where(p => p.DiscountPrice > 0);
            var result = _mapper.Map<IEnumerable<ProductDTO>>(product);
            foreach (ProductDTO productDTO in result)
            {
                var category = await GetCategoryWithId(productDTO.CategoryId);
                productDTO.Categories = category.Name;
                if (category.ParentId != null)
                {
                    var subCategory = await GetCategoryWithId((int)category.ParentId);
                    productDTO.SubCategory = subCategory.Name;
                }
                else productDTO.SubCategory = category.Name;
            }
            return result;
        }
        public async Task<IEnumerable<ProductDTO>> GetAllStockProduct()
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var product = _dbContext.Product.Include(p => p.ProductCategory).Include(p => p.ProductTranslates.Where(p => p.LanguageCulture == culture)).Where(p => p.IsStock == true);
            var result = _mapper.Map<IEnumerable<ProductDTO>>(product);
            foreach (ProductDTO productDTO in result)
            {
                var category = await GetCategoryWithId(productDTO.CategoryId);
                productDTO.Categories = category.Name;
                if (category.ParentId != null)
                {
                    var subCategory = await GetCategoryWithId((int)category.ParentId);
                    productDTO.SubCategory = subCategory.Name;
                }
                else productDTO.SubCategory = category.Name;
            }
            return result;
        }
        public async Task<IEnumerable<ProductDTO>> GetAllNewProduct()
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var product = _dbContext.Product.Include(p => p.ProductCategory).Include(p => p.ProductTranslates.Where(p => p.LanguageCulture == culture)).Where(p => p.IsNew == true);
            var result = _mapper.Map<IEnumerable<ProductDTO>>(product);
            foreach (ProductDTO productDTO in result)
            {
                var category = await GetCategoryWithId(productDTO.CategoryId);
                productDTO.Categories = category.Name;
                if (category.ParentId != null)
                {
                    var subCategory = await GetCategoryWithId((int)category.ParentId);
                    productDTO.SubCategory = subCategory.Name;
                }
                else productDTO.SubCategory = category.Name;
            }
            return result;
        }
        public async Task<ProductDetailDTO> GetProductWithId(int id)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var product = await _dbContext.Product.Include(p => p.ProductCategory).Include(p => p.ProductTranslates.Where(p => p.LanguageCulture == culture)).SingleOrDefaultAsync(p => p.ProductId == id);
            var result = _mapper.Map<ProductDetailDTO>(product);
            return result;
        }
        public async Task<IEnumerable<ProductDTO>> GetAllProductWithCategoryId(int id)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var product = _dbContext.ProductCategory.Include(p => p.Category).Where(p => p.CategoryId == id || p.Category.ParentId == id || p.Category.ParentParentId == id).Include(p => p.Product).ThenInclude(p => p.ProductTranslates.Where(p => p.LanguageCulture == culture));
            var result = _mapper.Map<IEnumerable<ProductDTO>>(product);
            foreach (ProductDTO productDTO in result)
            {
                var category = await GetCategoryWithId(productDTO.CategoryId);
                productDTO.Categories = category.Name;
                if (category.ParentId != null)
                {
                    var subCategory = await GetCategoryWithId((int)category.ParentId);
                    productDTO.SubCategory = subCategory.Name;
                }
                else productDTO.SubCategory = category.Name;
            }
            return result;
        }
        public async Task<IEnumerable<ProductDTO>> GetAllProductWithBrandId(int id, int brandId)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var product = _dbContext.ProductCategory.Include(p => p.Category).Include(p => p.Product).ThenInclude(p => p.ProductTranslates.Where(p => p.LanguageCulture == culture)).Where(p => (p.CategoryId == id || p.Category.ParentId == id || p.Category.ParentParentId == id) && (p.Product.BrandId == brandId));
            var result = _mapper.Map<IEnumerable<ProductDTO>>(product);
            foreach (ProductDTO productDTO in result)
            {
                var category = await GetCategoryWithId(productDTO.CategoryId);
                productDTO.Categories = category.Name;
                if (category.ParentId != null)
                {
                    var subCategory = await GetCategoryWithId((int)category.ParentId);
                    productDTO.SubCategory = subCategory.Name;
                }
                else productDTO.SubCategory = category.Name;
                if (category.ParentParentId != null)
                {
                    var category1 = await GetCategoryWithId((int)category.ParentParentId);
                    productDTO.ParentCategory = category1.Name;
                }
                else productDTO.ParentCategory = category.Name;
            }
            return result;
        }
        public IEnumerable<ProductDTO> GetProductWithSearch(string sText)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var product = _dbContext.Product.Include(p => p.ProductTranslates.Where(p => p.LanguageCulture == culture && p.Name.ToUpper() == sText.ToUpper()));
            //.Include(p => p.ProductCategory).ThenInclude(p => p.Category).ThenInclude(p => p.CategoryTranslates.Where(p => p.LanguageCulture == culture && p.Name.Contains(sText)));
            var result = _mapper.Map<IEnumerable<ProductDTO>>(product);
            return result;
        }
    }
}
