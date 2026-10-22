using AccountingProject.Models;
using AccountingProject.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AccountingProject.Controllers
{
	[AutoValidateAntiforgeryToken]      // Tüm POST isteklerinde anti-forgery token doğrulanır
	public class DashboardController : Controller
	{
		private readonly IDashboardService _service;

		public DashboardController(IDashboardService service) => _service = service;

		// GET /Dashboard?month=2026-09&filter=gider
		public async Task<IActionResult> Index(string? month, string? filter)
			=> View(await _service.GetDashboardAsync(month, filter));

		// Parametre adları formdaki alan adlarıyla eşleşir (büyük/küçük harf fark etmez)
		[HttpPost]
		public async Task<IActionResult> AddTransaction(int categoryId, decimal amount, DateTime date, string? note)
		{
			var ok = await _service.AddTransactionAsync(categoryId, amount, date, note);
			return Done(ok, "Kayıt eklendi.", ok ? date.ToString("yyyy-MM") : null);   // Eklenen kaydın olduğu aya geç
		}

		[HttpPost]
		public async Task<IActionResult> DeleteTransaction(int id, string? month, string? filter)
			=> Done(await _service.DeleteTransactionAsync(id), "Kayıt silindi.", month, filter);

		[HttpPost]
		public async Task<IActionResult> SetPool(decimal poolAmount, string? month)
		{
			await _service.SetPoolAsync(poolAmount);
			return Done(true, "Havuz bütçesi güncellendi.", month);
		}

		[HttpPost]
		public async Task<IActionResult> AddFixedExpense(decimal amount, int categoryId, int dayOfMonth, string? month)
			=> Done(await _service.AddFixedExpenseAsync(amount, categoryId, dayOfMonth), "Sabit gider eklendi.", month);

		[HttpPost]
		public async Task<IActionResult> DeleteFixedExpense(int id, string? month)
			=> Done(await _service.DeleteFixedExpenseAsync(id), "Sabit gider silindi.", month);

		[HttpPost]
		public async Task<IActionResult> PostFixedExpenses(string? month)
		{
			var count = await _service.PostFixedExpensesAsync(month);
			return Done(count > 0, $"{count} sabit gider işlendi.", month);
		}

		[HttpPost]
		public async Task<IActionResult> AddDebt(string creditor, decimal totalAmount, DateTime? dueDate, string? month)
			=> Done(await _service.AddDebtAsync(creditor, totalAmount, dueDate), "Borç eklendi.", month);

		[HttpPost]
		public async Task<IActionResult> PayDebt(int debtId, decimal amount)   // Ödeme bugünün tarihiyle kaydedilir, bu aya geç
			=> Done(await _service.PayDebtAsync(debtId, amount), "Borç ödemesi kaydedildi.", DateTime.Today.ToString("yyyy-MM"));

		[HttpPost]
		public async Task<IActionResult> DeleteDebt(int id, string? month)
			=> Done(await _service.DeleteDebtAsync(id), "Borç silindi.", month);

		// Mesajı TempData'ya yazıp Index'e döner
		private IActionResult Done(bool ok, string message, string? month, string? filter = null)
		{
			TempData[ok ? "Success" : "Error"] = ok ? message : "İşlem yapılamadı. Girilen bilgileri kontrol edin.";
			return RedirectToAction(nameof(Index), new { month, filter });
		}
	}
}
