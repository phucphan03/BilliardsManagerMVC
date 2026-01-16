using BusinessObject.FacadeService;
using DataAccessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BilliardsManager.Controllers
{
    public class TableSessionController : Controller
    {
        private readonly IFacadeService _facadeService;
        public TableSessionController(IFacadeService facadeService)
        {
            _facadeService = facadeService;
        }

        public async Task<IActionResult> Detail(Guid tableId)
        {
            var tableSession = await _facadeService.TableSessionService
                .GetTableSessionByTableIdAsync(tableId);
            if (tableSession == null)
            {
                return NotFound();
            }
            var products = await _facadeService.ProductService.GetAllProductAsync();

            ViewBag.Products = products.Select(p => new SelectListItem
            {
                Value = p.ProductID.ToString(),
                Text = $"{p.Name} - {p.Price:N0} VNĐ"
            }).ToList();

            // Tính tiền bàn theo giờ
            var startTime = tableSession.StartTime;
            var endTime = tableSession.EndTime ?? DateTime.Now;
            var duration = endTime - startTime;
            var totalHours = duration.TotalHours;
            var moneyPerHour = 50000;
            var tableAmount = (decimal)(totalHours * moneyPerHour);

            // Tổng tiền sản phẩm
            var productAmount = tableSession.TableProducts?.Sum(tp => tp.SubTotal) ?? 0;

            // Tổng tiền
            var totalAmount = tableAmount + productAmount;

            ViewBag.TableAmount = tableAmount;
            ViewBag.ProductAmount = productAmount;
            ViewBag.TotalAmount = totalAmount;
            ViewBag.StartTime = startTime;
            ViewBag.EndTime = endTime;
            ViewBag.DurationHours = (int)duration.TotalHours;
            ViewBag.DurationMinutes = duration.TotalMinutes;
            return View(tableSession);
        }

        public async Task<IActionResult> CreateTableSession(TableSession tableSession, Guid tableId)
        {
            await _facadeService.TableSessionService.CreateTableSessionAsync(tableSession, tableId);
            TempData["success"] = "Bắt đầu phiên chơi thành công.";
            return RedirectToAction("Index", "Table");
        }

        public async Task<IActionResult> PaymentTableSession(Guid tableSessionId, Guid? tableId)
        {
            if (tableId == null)
            {
                TempData["error"] = "Bàn chưa có phiên chơi.";
                return RedirectToAction("Detail", new { tableId });
            }
            await _facadeService.TableSessionService.PaymentAsync(tableSessionId);
            TempData["success"] = "Chờ thanh toán.";
            return RedirectToAction("Detail", new { tableId });
        }

        public async Task<IActionResult> OKTableSession(Guid tableSessionId)
        {
            await _facadeService.TableSessionService.OKAsync(tableSessionId);
            return RedirectToAction("Index", "Table");
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToTableSession(
            Guid? tableId, Guid tableSessionId, Guid productId, int quantity
        )
        {
            if (tableId == null)
            {
                TempData["error"] = "Bàn chưa có phiên chơi.";
                return RedirectToAction("Detail", new { tableId });
            }
            await _facadeService.TableProductService.AddProductAsync(tableSessionId, productId, quantity);

            TempData["success"] = "Thêm sản phẩm thành công.";
            return RedirectToAction("Detail", new { tableId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductFromTableSession(
            Guid tableProductId, Guid? tableId, Guid tableSessionId
        )
        {
            if (tableId == null)
            {
                TempData["error"] = "Bàn chưa có phiên chơi.";
                return RedirectToAction("Detail", new { tableId });
            }
            await _facadeService.TableProductService
                .DeleteProductAsync(tableProductId, tableSessionId);

            TempData["success"] = "Xóa sản phẩm thành công.";
            return RedirectToAction("Detail", new { tableId });
        }
    }
}
