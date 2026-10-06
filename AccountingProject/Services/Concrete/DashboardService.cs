using AccountingProject.Datas.Repositories.Abstract;
using AccountingProject.Entities;
using AccountingProject.Models;
using AccountingProject.Services.Abstract;
using System.Globalization;
using Type = AccountingProject.Entities.Type;

namespace AccountingProject.Services.Concrete
{
	public class DashboardService : IDashboardService
	{
		private static readonly CultureInfo Tr = new("tr-TR");
		private readonly ITransactionRepository _transactions;
		private readonly ICategoryRepository _categories;

		public DashboardService(ITransactionRepository transactions, ICategoryRepository categories)
		{
			_transactions = transactions;
			_categories = categories;
		}

		public async Task<DashboardViewModel> GetDashboardAsync(string? month, string? filter)
		{
			var selected = ParseMonth(month);
			var from = selected.AddMonths(-11);      // son 12 ayın ilk günü
			var to = selected.AddMonths(1);          // seçili ayın bitişi (hariç)
			var f = filter is "gelir" or "gider" ? filter : "hepsi";

			// Transaction'da Type yok; tür Category.Type'tan gelir. Kategori tablosu küçük olduğu için sözlüğe alıyoruz.
			var cats = (await _categories.GetAllAsync()).ToDictionary(c => c.Id);
			var all = (await _transactions.FindAsync(t => t.TransactionDate >= from && t.TransactionDate < to)).ToList();

			decimal Sum(IEnumerable<Transaction> l, Type type) =>
				l.Where(t => cats[t.CategoryId].Type == type).Sum(t => t.Amount);
			List<Transaction> InMonth(DateTime m) =>
				all.Where(t => t.TransactionDate.Year == m.Year && t.TransactionDate.Month == m.Month).ToList();
			static decimal? Change(decimal cur, decimal prev) => prev == 0 ? null : Math.Round((cur - prev) / prev * 100);

			var cur = InMonth(selected);
			var prev = InMonth(selected.AddMonths(-1));
			var income = Sum(cur, Type.Income);
			var expense = Sum(cur, Type.Expense);

			var trend = Enumerable.Range(0, 12).Select(i =>
			{
				var m = from.AddMonths(i);
				var l = InMonth(m);
				var inc = Sum(l, Type.Income);
				var exp = Sum(l, Type.Expense);
				return new MonthlyTrendItem { Label = m.ToString("MMM yy", Tr), Income = inc, Expense = exp, Net = inc - exp, HasData = l.Count > 0 };
			}).ToList();
			var active = Math.Max(1, trend.Count(x => x.HasData));

			var expenseTx = cur.Where(t => cats[t.CategoryId].Type == Type.Expense).ToList();
			var biggest = expenseTx.OrderByDescending(t => t.Amount).FirstOrDefault();

			return new DashboardViewModel
			{
				Month = selected.ToString("yyyy-MM"),
				Filter = f,
				TotalIncome = income,
				TotalExpense = expense,
				IncomeChange = Change(income, Sum(prev, Type.Income)),
				ExpenseChange = Change(expense, Sum(prev, Type.Expense)),
				ActiveMonthCount = active,
				AvgIncome = trend.Sum(x => x.Income) / active,
				AvgExpense = trend.Sum(x => x.Expense) / active,
				BiggestAmount = biggest?.Amount,
				BiggestCategory = biggest is null ? null : cats[biggest.CategoryId].Name,
				BiggestNote = biggest?.Note,
				Trend = trend,
				ExpenseByCategory = expenseTx.GroupBy(t => t.CategoryId)
					.Select(g => new CategoryTotalItem
					{
						Name = cats[g.Key].Name,
						Total = g.Sum(x => x.Amount),
						Percent = expense > 0 ? (int)Math.Round(g.Sum(x => x.Amount) / expense * 100) : 0
					})
					.OrderByDescending(x => x.Total).ToList(),
				Transactions = cur
					.Where(t => f == "hepsi" || (f == "gelir") == (cats[t.CategoryId].Type == Type.Income))
					.OrderByDescending(t => t.TransactionDate).ThenByDescending(t => t.Id)
					.Select(t => new TransactionListItem
					{
						Id = t.Id,
						Date = t.TransactionDate,
						CategoryName = cats[t.CategoryId].Name,
						IsIncome = cats[t.CategoryId].Type == Type.Income,
						Amount = t.Amount,
						Note = t.Note
					}).ToList(),
				Categories = cats.Values.OrderBy(c => c.Type).ThenBy(c => c.Id)
					.Select(c => new CategoryOption { Id = c.Id, Name = c.Name, Type = c.Type == Type.Income ? "gelir" : "gider" })
					.ToList()
			};
		}

		public async Task<bool> AddTransactionAsync(TransactionCreateViewModel model)
		{
			if (await _categories.GetByIdAsync(model.CategoryId) is null) return false;

			await _transactions.AddAsync(new Transaction
			{
				CategoryId = model.CategoryId,
				Amount = Math.Round(model.Amount, 2),
				TransactionDate = model.Date!.Value.Date,
				Note = string.IsNullOrWhiteSpace(model.Note) ? null : model.Note.Trim()
			});
			await _transactions.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteTransactionAsync(int id)
		{
			var entity = await _transactions.GetByIdAsync(id);
			if (entity is null) return false;

			_transactions.Remove(entity);
			await _transactions.SaveChangesAsync();
			return true;
		}

		private static DateTime ParseMonth(string? m) =>
			DateTime.TryParseExact(m + "-01", "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var d)
				? d
				: new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

	}
}
