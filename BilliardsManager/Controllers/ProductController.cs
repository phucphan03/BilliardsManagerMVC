using BusinessObject.FacadeService;
using DataAccessObject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BilliardsManager.Controllers
{
    public class ProductController : Controller
    {
        private readonly IFacadeService _facadeService;
        public ProductController(IFacadeService facadeService)
        {
            _facadeService = facadeService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllProduct()
        {
            var products = (await _facadeService.ProductService.GetAllProductAsync())
                .Select(p => new
                {
                    p.ProductID,
                    p.Name,
                    p.Price,
                    CategoryName = p.Category != null ? p.Category.Name : "N/A",
                    ImageUrl = p.ProductImage != null ? p.ProductImage.ImageUrl : null
                })
                .ToList();
            return Json(new { data = products });
        }

        public async Task<IActionResult> AddProduct()
        {
            var categories = await _facadeService.CategoryService.GetAllCategoriesAsync();
            ViewBag.Categories = categories ?? new List<DataAccessObject.Models.Category>();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product, IFormFile? ProductImage)
        {
            if (ModelState.IsValid)
            {
                await _facadeService.ProductService.AddProductAsync(product, ProductImage);
                TempData["success"] = "Tạo sản phẩm thành công";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Tạo sản phẩm thất bại";            
            return View(product);
        }

        public async Task<IActionResult> EditProduct(Guid id)
        {
            var product = await _facadeService.ProductService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var categories = await _facadeService.CategoryService.GetAllCategoriesAsync();
            ViewBag.Categories = categories ?? new List<DataAccessObject.Models.Category>();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product, IFormFile? ProductImage)
        {
            if (ModelState.IsValid)
            {
                await _facadeService.ProductService.UpdateProductAsync(product, ProductImage);
                TempData["success"] = "Cập nhật sản phẩm thành công";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Cập nhật sản phẩm thất bại";            
            var categories = await _facadeService.CategoryService.GetAllCategoriesAsync();
            ViewBag.Categories = categories ?? new List<DataAccessObject.Models.Category>();
            return View(product);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            if(id.ToString() == null)
            {
                return Json(new { success = false, message = "ID sản phẩm không hợp lệ" });
            }
            await _facadeService.ProductService.DeleteProductAsync(id);
            return Json(new { success = true, message = "Xóa sản phẩm thành công" });
        }
    }
}
