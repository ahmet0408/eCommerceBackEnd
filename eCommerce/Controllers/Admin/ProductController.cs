using AspNetCoreHero.ToastNotification.Abstractions;
using eCommerce.bll.DTOs.ProductDTO;
using eCommerce.bll.Services.LanguageService;
using eCommerce.bll.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Controllers.Admin
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ILanguageService _languageService;
        private readonly IProductService _productService;
        private readonly INotyfService _notyf;
        public ProductController(ILanguageService languageService, IProductService productService, INotyfService notyf)
        {
            _languageService = languageService;
            _productService = productService;
            _notyf = notyf;
        }
        [HttpGet]
        public IActionResult CategoryList()
        {
            //_notyf.Success("Success Notification");
            //_notyf.Success("Success Notification that closes in 10 Seconds.", 10);
            //_notyf.Error("Some Error Message");
            //_notyf.Warning("Some Error Message");
            //_notyf.Information("Information Notification - closes in 4 seconds.", 4);
            //_notyf.Custom("Custom Notification <br><b><i>closes in 5 seconds.</i></b></p>", 5, "indigo", "fa fa-gear");
            //_notyf.Custom("Custom Notification - closes in 5 seconds.", 5, "whitesmoke", "fa fa-gear");
            //_notyf.Custom("Custom Notification - closes in 10 seconds.", 10, "#B600FF", "fa fa-home");
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View("../Admin/Product/CategoryList");
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View("../Admin/Product/CreateCategory");
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateCategory(category);
                _notyf.Success("Kategoriýa goşuldy");
                return RedirectToAction("CategoryList");
            }
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            _notyf.Error("Ýalňyşlyk");
            return View("../Admin/Product/CreateCategory");
        }
        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _productService.GetCategoryForEditById(id);

            if (category == null)
            {
                return NotFound();
            }
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View("../Admin/Product/EditCategory", category);
        }
        [HttpPost]
        public async Task<IActionResult> EditCategory(EditCategoryDTO editCategoryDTO)
        {
            if (ModelState.IsValid)
            {
                await _productService.EditCategory(editCategoryDTO);
                _notyf.Success("Kategoriýa üýtgedildi");
                return RedirectToAction("CategoryList");
            }
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            _notyf.Error("Ýalňyşlyk");
            return View("../Admin/Product/EditCategory", editCategoryDTO);
        }
        //Brand
        [HttpGet]
        public IActionResult Brand()
        {
            ViewBag.Category = _productService.GetAllCategoryWithoutParent().OrderBy(o => o.Order);
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View("../Admin/Product/Brand");
        }
        [HttpGet]
        public IActionResult CreateBrand()
        {
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View("../Admin/Product/CreateBrand");
        }
        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDTO brand)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateBrand(brand);
                _notyf.Success("Marka goşuldy");
                return RedirectToAction("Brand");
            }
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            _notyf.Error("Ýalňyşlyk");
            return View("../Admin/Product/CreateBrand");
        }
        [HttpGet]
        public async Task<IActionResult> EditBrand(int id)
        {
            var brand = await _productService.GetBrandForEditById(id);
            if (brand == null)
            {
                return NotFound();
            }
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View("../Admin/Product/EditBrand", brand);
        }
        [HttpPost]
        public async Task<IActionResult> EditBrand(EditBrandDTO editBrandDTO)
        {
            if (ModelState.IsValid)
            {
                await _productService.EditBrand(editBrandDTO);
                _notyf.Success("Marka üýtgedildi");
                return RedirectToAction("Brand");
            }
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            _notyf.Error("Ýalňyşlyk");
            return View("../Admin/Product/EditBrand", editBrandDTO);
        }
        //Product        
        [HttpGet]
        public IActionResult Category(int list)
        {
            ViewBag.List = list;
            ViewBag.Category = _productService.GetAllCategoryWithoutParent().OrderBy(o => o.Order);
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View("../Admin/Product/Category");
        }
        [HttpGet]
        public IActionResult SubCategory(int id, int list)
        {
            ViewBag.List = list;
            ViewBag.Category = _productService.GetAllCategoryWithoutParent().OrderBy(o => o.Order);
            ViewBag.SubCategory = _productService.GetAllSubCategoryWithParentId(id).OrderBy(o => o.Order);
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View("../Admin/Product/SubCategory");
        }
        [HttpGet]
        public async Task<IActionResult> Product(int id, int parent)
        {
            ViewBag.ParentId = id; ViewBag.Parent = parent;
            ViewBag.Cat = await _productService.GetCategoryWithId(id);
            ViewBag.Category = _productService.GetAllCategoryWithoutParent().OrderBy(o => o.Order);
            ViewBag.SubCategory = _productService.GetAllSubCategoryWithParentId(parent).OrderBy(o => o.Order);
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View("../Admin/Product/Product");
        }
        [HttpGet]
        public IActionResult CreateProduct(int id, int parent)
        {
            ViewBag.ParentId = id; ViewBag.Parent = parent;
            ViewBag.Category = _productService.GetAllCategoryWithoutParent().OrderBy(o => o.Order);
            ViewBag.SubCategory = _productService.GetAllSubCategoryWithParentId(parent).OrderBy(o => o.Order);
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View("../Admin/Product/CreateProduct");
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(int id, int parent, CreateProductDTO product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProduct(product);
                _notyf.Success("Haryt goşuldy");
                return Redirect("/Product/Product/" + id.ToString() + "?parent=" + parent.ToString());
            }
            ViewBag.ParentId = id; ViewBag.Parent = parent;
            ViewBag.Category = _productService.GetAllCategoryWithoutParent().OrderBy(o => o.Order);
            ViewBag.SubCategory = _productService.GetAllSubCategoryWithParentId(parent).OrderBy(o => o.Order);
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            _notyf.Error("Ýalňyşlyk");
            return View("../Admin/Product/CreateProduct");
        }
        [HttpGet]
        public async Task<IActionResult> EditProduct(int id, int parent, int child)
        {
            var product = await _productService.GetProductForEditById(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.ParentId = child; ViewBag.Parent = parent;
            ViewBag.Category = _productService.GetAllCategoryWithoutParent().OrderBy(o => o.Order);
            ViewBag.SubCategory = _productService.GetAllSubCategoryWithParentId(parent).OrderBy(o => o.Order);
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View("../Admin/Product/EditProduct", product);
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(int parent, int child, EditProductDTO editProductDTO)
        {
            if (ModelState.IsValid)
            {
                await _productService.EditProduct(editProductDTO);
                _notyf.Success("Haryt üýtgedildi");
                return Redirect("/Product/Product/" + child.ToString() + "?parent=" + parent.ToString());
            }
            ViewBag.ParentId = child; ViewBag.Parent = parent;
            ViewBag.Category = _productService.GetAllCategoryWithoutParent().OrderBy(o => o.Order);
            ViewBag.SubCategory = _productService.GetAllSubCategoryWithParentId(parent).OrderBy(o => o.Order);
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            _notyf.Error("Ýalňyşlyk");
            return View("../Admin/Product/EditProduct", editProductDTO);
        }
    }
}
