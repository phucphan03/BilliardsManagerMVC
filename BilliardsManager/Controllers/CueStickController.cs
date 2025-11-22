using BusinessObject.FacadeService;
using DataAccessObject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BilliardsManager.Controllers
{
    public class CueStickController : Controller
    {
        private readonly IFacadeService _facadeService;
        public CueStickController(IFacadeService facadeService)
        {
            _facadeService = facadeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllCueStick()
        {
            var cueSticks = (await _facadeService.CueStickService.GetAllCueSticksAsync())
                .Select(c => new
                {
                    c.CueStickID,
                    c.Name,
                    c.Brand,
                    c.PricePerTurn,
                    ImageUrl = c.CueStickImage != null ? c.CueStickImage.ImageUrl : null
                }).ToList();
            return Json(new { data = cueSticks });
        }

        public IActionResult AddCueStick()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCueStick(CueStick cueStick, IFormFile? CueStickImage)
        {
            if (ModelState.IsValid)
            {
                await _facadeService.CueStickService.AddCueStickAsync(cueStick, CueStickImage);
                TempData["success"] = "Tạo cơ thành công";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Tạo cơ thất bại";
            return View(cueStick);
        }

        public async Task<IActionResult> EditCueStick(Guid id)
        {
            var cueStick = await _facadeService.CueStickService.GetCueStickByIdAsync(id);
            return View(cueStick);
        }

        [HttpPost]
        public async Task<IActionResult> EditCueStick(CueStick cueStick, IFormFile? CueStickImage)
        {
            if (ModelState.IsValid)
            {
                await _facadeService.CueStickService.UpdateCueStickAsync(cueStick, CueStickImage);
                TempData["success"] = "Cập nhật cơ thành công";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Cập nhật cơ thất bại";
            return View(cueStick);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCueStick(Guid id)
        {
            var existingCueStick = await _facadeService.CueStickService.GetCueStickByIdAsync(id);
            if (existingCueStick == null)
            {
                return Json(new { success = false, message = "Cơ không tồn tại" });
            }
            await _facadeService.CueStickService.DeleteCueStickAsync(id);
            return Json(new { success = true, message = "Xóa cơ thành công" });
        }
    }
}