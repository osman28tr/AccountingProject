using AccountingProject.Models;
using AccountingProject.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AccountingProject.Controllers
{
	public class DashboardController : Controller
	{
		private readonly IDashboardService _service;

		public DashboardController(IDashboardService service) => _service = service;

		// GET /Dashboard?month=2026-09&filter=gider
		public async Task<IActionResult> Index(string? month, string? filter)
		{
			var model = await _service.GetDashboardAsync(month, filter);
			return View(model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(TransactionCreateViewModel model)
		{
			if (!ModelState.IsValid || !await _service.AddTransactionAsync(model))
			{
				TempData["Error"] = "Kayıt eklenemedi. Kategori, tutar ve tarihi kontrol edin.";
				return RedirectToAction(nameof(Index));
			}

			TempData["Success"] = "Kayıt eklendi.";
			// Eklenen kaydın olduğu aya geç
			return RedirectToAction(nameof(Index), new { month = model.Date!.Value.ToString("yyyy-MM") });
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id, string? month, string? filter)
		{
			if (await _service.DeleteTransactionAsync(id)) TempData["Success"] = "Kayıt silindi.";
			else TempData["Error"] = "Kayıt bulunamadı.";
			return RedirectToAction(nameof(Index), new { month, filter });
		}
	}
}
