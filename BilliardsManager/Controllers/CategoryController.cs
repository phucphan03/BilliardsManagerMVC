using BusinessObject.FacadeService;
using DataAccessObject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BilliardsManager.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IFacadeService _facadeService;
        public CategoryController(IFacadeService facadeService)
        {
            _facadeService = facadeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllCategories()
        {
            var categories = (await _facadeService.CategoryService
                .GetAllCategoriesAsync(includeProperties: "Products"))
                .Select(c => new
                {
                    c.CategoryID,
                    c.Name,
                    ProductCount = c.Products?.Count(),
                })
                .ToList();
            return Json(new { data = categories });
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                await _facadeService.CategoryService.AddCategoryAsync(category);
                TempData["success"] = "Tạo danh mục thành công";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Tạo danh mục thất bại";
            return View(category);
        }

        public async Task<IActionResult> EditCategory(Guid id)
        {
            var category = await _facadeService.CategoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                TempData["error"] = "Lỗi! Không tìm thấy danh mục";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                await _facadeService.CategoryService.UpdateCategoryAsync(category);
                TempData["success"] = "Cập nhật danh mục thành công";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Cập nhật danh mục thất bại";
            return View(category);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _facadeService.CategoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return Json(new { success = false, message = "Lỗi! Không tìm thấy danh mục" });
            }
            await _facadeService.CategoryService.DeleteCategoryAsync(id);
            return Json(new { success = true, message = "Xóa danh mục thành công" });
        }
    }
}
