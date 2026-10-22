using AccountingProject.Datas.Repositories.Abstract;
using AccountingProject.Entities;
using AccountingProject.Models;
using AccountingProject.Services.Abstract;
using System.Globalization;
using Type = AccountingProject.Entities.TransactionType;

namespace AccountingProject.Services.Concrete
{	

	// Not: Repository'ler aynı scoped DbContext'i paylaşır; herhangi birinin SaveChangesAsync'i tüm değişiklikleri kaydeder.
	public class DashboardService : IDashboardService
	{
		private static readonly CultureInfo Tr = new("tr-TR");
		private const string DebtCategory = "Borç ödemesi";

		private readonly ITransactionRepository _transactions;
		private readonly ICategoryRepository _categories;
		private readonly IBudgetRepository _budgets;
		private readonly IFixedExpenseRepository _fixedExpenses;
		private readonly IDebtRepository _debts;

		public DashboardService(ITransactionRepository transactions, ICategoryRepository categories,
			IBudgetRepository budgets, IFixedExpenseRepository fixedExpenses, IDebtRepository debts)
		{
			_transactions = transactions; _categories = categories; _budgets = budgets;
			_fixedExpenses = fixedExpenses; _debts = debts;
		}

		// ---------- Dashboard ----------
		public async Task<DashboardViewModel> GetDashboardAsync(string? month, string? filter)
		{
			var selected = ParseMonth(month);
			var from = selected.AddMonths(-11);
			var to = selected.AddMonths(1);
			filter = filter is "gelir" or "gider" ? filter : "hepsi";

			var cats = (await _categories.GetAllAsync()).ToDictionary(c => c.Id);
			var last12 = (await _transactions.FindAsync(t => t.TransactionDate >= from && t.TransactionDate < to)).ToList();
			var debtPayments = (await _transactions.FindAsync(t => t.DebtId != null)).ToList();

			// Transaction'da tür yok; gelir mi gider mi olduğu kategoriden gelir
			bool IsIncome(Transaction t) => cats[t.CategoryId].Type == TransactionType.Income;
			decimal Total(IEnumerable<Transaction> items, bool income) => items.Where(t => IsIncome(t) == income).Sum(t => t.Amount);
			List<Transaction> InMonth(DateTime m) => last12.Where(t => t.TransactionDate.Year == m.Year && t.TransactionDate.Month == m.Month).ToList();
			TransactionItem ToItem(Transaction t) => new(t.Id, t.TransactionDate, cats[t.CategoryId].Name, IsIncome(t), t.Amount, t.Note);
			static decimal? Change(decimal now, decimal before) => before == 0 ? null : Math.Round((now - before) / before * 100);

			var current = InMonth(selected);
			var previous = InMonth(selected.AddMonths(-1));
			var expenses = current.Where(t => !IsIncome(t)).ToList();
			var income = Total(current, true);
			var expense = Total(current, false);

			var trend = Enumerable.Range(0, 12).Select(i =>
			{
				var m = selected.AddMonths(i - 11);
				var items = InMonth(m);
				return new MonthlyTrendItem(m.ToString("MMM yy", Tr), Total(items, true), Total(items, false));
			}).ToList();
			var activeMonths = Math.Max(1, trend.Count(x => x.Income + x.Expense > 0));   // Ortalama için yalnızca işlem olan aylar

			var biggest = expenses.OrderByDescending(t => t.Amount).FirstOrDefault();
			var budget = (await _budgets.GetAllAsync()).FirstOrDefault();
			var thisYear = last12.Where(t => t.TransactionDate.Year == selected.Year).ToList();

			return new DashboardViewModel
			{
				Month = selected.ToString("yyyy-MM"),
				Filter = filter,
				TotalIncome = income,
				TotalExpense = expense,
				IncomeChange = Change(income, Total(previous, true)),
				ExpenseChange = Change(expense, Total(previous, false)),
				BiggestExpense = biggest is null ? null : ToItem(biggest),
				ExpenseByCategory = expenses.GroupBy(t => cats[t.CategoryId].Name)
					.Select(g => new CategoryTotalItem(g.Key, g.Sum(t => t.Amount), (int)Math.Round(g.Sum(t => t.Amount) / expense * 100)))
					.OrderByDescending(c => c.Total).ToList(),

				Trend = trend,
				AvgIncome = trend.Sum(x => x.Income) / activeMonths,
				AvgExpense = trend.Sum(x => x.Expense) / activeMonths,

				PoolAmount = budget?.PoolAmount ?? 0,
				PoolUsed = Total(thisYear, false),

				FixedExpenses = (await _fixedExpenses.GetAllAsync()).OrderBy(f => f.DayOfMonth)
					.Select(f => new FixedExpenseItem(f.Id, cats[f.CategoryId].Name, f.Amount, f.DayOfMonth,
						current.Any(t => t.FixedExpenseId == f.Id))).ToList(),

				// Kalan borç saklanmaz: toplam borç - bu borca bağlı ödemeler
				Debts = (await _debts.GetAllAsync())
					.Select(d => new DebtItem(d.Id, d.Creditor, d.TotalAmount,
						Math.Max(0, d.TotalAmount - debtPayments.Where(p => p.DebtId == d.Id).Sum(p => p.Amount)), d.DueDate)).ToList(),

				Transactions = current.Where(t => filter == "hepsi" || IsIncome(t) == (filter == "gelir"))
					.OrderByDescending(t => t.TransactionDate).ThenByDescending(t => t.Id).Select(ToItem).ToList(),
				Categories = cats.Values.OrderBy(c => c.Type).ThenBy(c => c.Id)
					.Select(c => new CategoryOption(c.Id, c.Name, c.Type == TransactionType.Income)).ToList()
			};
		}

		// ---------- Gelir / gider ----------
		public async Task<bool> AddTransactionAsync(int categoryId, decimal amount, DateTime date, string? note)
		{
			if (amount <= 0 || date == default || await _categories.GetByIdAsync(categoryId) is null) return false;

			await _transactions.AddAsync(new Transaction
			{
				CategoryId = categoryId,
				Amount = Math.Round(amount, 2),
				TransactionDate = date.Date,
				Note = string.IsNullOrWhiteSpace(note) ? null : note.Trim()
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

		// ---------- Havuz bütçesi (tabloda tek satır) ----------
		public async Task SetPoolAsync(decimal amount)
		{
			var budget = (await _budgets.GetAllAsync()).FirstOrDefault() ?? new Budget();
			budget.PoolAmount = Math.Max(0, amount);
			if (budget.Id == 0) await _budgets.AddAsync(budget); else _budgets.Update(budget);
			await _budgets.SaveChangesAsync();
		}

		// ---------- Sabit giderler ----------
		public async Task<bool> AddFixedExpenseAsync(decimal amount, int categoryId, int dayOfMonth)
		{
			var category = await _categories.GetByIdAsync(categoryId);
			if (amount <= 0 || dayOfMonth is < 1 or > 31
				|| category is null || category.Type == TransactionType.Income) return false;   // Gider veya sabit gider kategorisi olmalı

			await _fixedExpenses.AddAsync(new FixedExpense { Amount = Math.Round(amount, 2), CategoryId = categoryId, DayOfMonth = dayOfMonth });
			await _fixedExpenses.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteFixedExpenseAsync(int id)
		{
			var entity = await _fixedExpenses.GetByIdAsync(id);
			if (entity is null) return false;
			_fixedExpenses.Remove(entity);           // Önceden işlenmiş kayıtlar silinmez
			await _fixedExpenses.SaveChangesAsync();
			return true;
		}

		// Seçili ay için henüz işlenmemiş sabit giderlerden gider kaydı oluşturur
		public async Task<int> PostFixedExpensesAsync(string? month)
		{
			var start = ParseMonth(month);
			var end = start.AddMonths(1);
			var days = DateTime.DaysInMonth(start.Year, start.Month);

			var postedIds = (await _transactions.FindAsync(t => t.FixedExpenseId != null && t.TransactionDate >= start && t.TransactionDate < end))
				.Select(t => t.FixedExpenseId).ToList();
			var cats = (await _categories.GetAllAsync()).ToDictionary(c => c.Id);
			var pending = (await _fixedExpenses.GetAllAsync()).Where(f => !postedIds.Contains(f.Id)).ToList();

			foreach (var f in pending)
				await _transactions.AddAsync(new Transaction
				{
					CategoryId = f.CategoryId,
					Amount = f.Amount,
					FixedExpenseId = f.Id,
					Note = $"{cats[f.CategoryId].Name} (sabit gider)",
					TransactionDate = new DateTime(start.Year, start.Month, Math.Min(f.DayOfMonth, days))   // Kısa aylarda ayın son günü
				});

			if (pending.Count > 0) await _transactions.SaveChangesAsync();
			return pending.Count;
		}

		// ---------- Borçlar ----------
		public async Task<bool> AddDebtAsync(string creditor, decimal totalAmount, DateTime? dueDate)
		{
			if (string.IsNullOrWhiteSpace(creditor) || totalAmount <= 0) return false;

			await _debts.AddAsync(new Debt { Creditor = creditor.Trim(), TotalAmount = Math.Round(totalAmount, 2), DueDate = dueDate?.Date });
			await _debts.SaveChangesAsync();
			return true;
		}

		// Ödeme, "Borç ödemesi" kategorisinde bir gider kaydıdır
		public async Task<bool> PayDebtAsync(int debtId, decimal amount)
		{
			var debt = await _debts.GetByIdAsync(debtId);
			var category = (await _categories.FindAsync(c => c.Name == DebtCategory)).FirstOrDefault();
			if (amount <= 0 || debt is null || category is null) return false;

			var remaining = debt.TotalAmount - (await _transactions.FindAsync(t => t.DebtId == debtId)).Sum(t => t.Amount);
			if (remaining <= 0) return false;

			await _transactions.AddAsync(new Transaction
			{
				CategoryId = category.Id,
				DebtId = debtId,
				TransactionDate = DateTime.Today,
				Amount = Math.Min(Math.Round(amount, 2), remaining),      // Kalan borcu aşamaz
				Note = $"Borç ödemesi: {debt.Creditor}"
			});
			await _transactions.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteDebtAsync(int id)
		{
			var entity = await _debts.GetByIdAsync(id);
			if (entity is null) return false;
			_debts.Remove(entity);                   // Ödeme kayıtları silinmez
			await _debts.SaveChangesAsync();
			return true;
		}

		private static DateTime ParseMonth(string? month) =>
			DateTime.TryParseExact(month + "-01", "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var d)
				? d : new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
	}
}
