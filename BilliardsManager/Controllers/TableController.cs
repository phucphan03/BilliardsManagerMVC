using System.Diagnostics;
using BilliardsManager.Models;
using BusinessObject.FacadeService;
using DataAccessObject.Models;
using Microsoft.AspNetCore.Mvc;

namespace BilliardsManager.Controllers
{
    public class TableController : Controller
    {
        private readonly IFacadeService _facadeService;

        public TableController(IFacadeService facadeService)
        {
            _facadeService = facadeService;
        }

        public async Task<IActionResult> Index()
        {
            var tables = await _facadeService.TableService.GetAllTablesAsync();
            var playingTimes = tables
                .Where(tables => tables.Status == TableStatus.Playing && tables.TableSessions != null)
                .ToDictionary(
                    t => t.TableID,
                    t => t.TableSessions!.OrderByDescending(ts => ts.StartTime).FirstOrDefault()?.StartTime
                );
            ViewBag.PlayingTimes = playingTimes;
            return View(tables);
        }

        public IActionResult AddTable()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTable(Table table)
        {
            if (ModelState.IsValid)
            {
                await _facadeService.TableService.AddTableAsync(table);
                TempData["success"] = "T?o bàn bida thành công.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "T?o bàn bida th?t b?i.";
            return View(table);
        }

        public async Task<IActionResult> EditTable(Guid id)
        {
            var table = await _facadeService.TableService.GetTableByIdAsync(id);
            if (table == null)
            {
                return NotFound();
            }
            return View(table);
        }
        [HttpPost]
        public async Task<IActionResult> EditTable(Table table)
        {
            if (ModelState.IsValid)
            {
                await _facadeService.TableService.UpdateTableAsync(table);
                TempData["success"] = "C?p nh?t bàn bida thành công.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "C?p nh?t bàn bida th?t b?i.";
            return View(table);
        }
    }
}