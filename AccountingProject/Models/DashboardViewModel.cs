using System.ComponentModel.DataAnnotations;

namespace AccountingProject.Models
{
	public class DashboardViewModel
	{
		public string Month { get; set; } = "";              // yyyy-MM
		public string Filter { get; set; } = "hepsi";        // hepsi | gelir | gider

		// Seçili ay
		public decimal TotalIncome { get; set; }
		public decimal TotalExpense { get; set; }
		public decimal Net => TotalIncome - TotalExpense;
		public decimal? IncomeChange { get; set; }           // Geçen aya göre %, geçen ay 0 ise null
		public decimal? ExpenseChange { get; set; }
		public TransactionItem? BiggestExpense { get; set; }
		public List<CategoryTotalItem> ExpenseByCategory { get; set; } = new();

		// Son 12 ay
		public List<MonthlyTrendItem> Trend { get; set; } = new();
		public decimal AvgIncome { get; set; }
		public decimal AvgExpense { get; set; }
		public decimal AvgNet => AvgIncome - AvgExpense;

		// Havuz bütçesi
		public decimal PoolAmount { get; set; }
		public decimal PoolUsed { get; set; }                // Yıl başından seçili aya kadar toplam gider
		public decimal PoolRemaining => PoolAmount - PoolUsed;
		public int PoolUsedPercent => PoolAmount > 0 ? (int)Math.Round(PoolUsed / PoolAmount * 100) : 0;

		// Sabit giderler
		public List<FixedExpenseItem> FixedExpenses { get; set; } = new();
		public decimal FixedTotal => FixedExpenses.Sum(f => f.Amount);
		public decimal? FixedIncomePercent => TotalIncome > 0 ? Math.Round(FixedTotal / TotalIncome * 100) : null;

		// Borçlar
		public List<DebtItem> Debts { get; set; } = new();
		public decimal DebtRemaining => Debts.Sum(d => d.RemainingAmount);
		public int OpenDebtCount => Debts.Count(d => d.RemainingAmount > 0);
		public int DebtPaidPercent
		{
			get
			{
				var total = Debts.Sum(d => d.TotalAmount);
				return total > 0 ? (int)Math.Round((total - DebtRemaining) / total * 100) : 0;
			}
		}

		public List<TransactionItem> Transactions { get; set; } = new();
		public List<CategoryOption> Categories { get; set; } = new();
	}

	public record MonthlyTrendItem(string Label, decimal Income, decimal Expense) { public decimal Net => Income - Expense; }
	public record CategoryTotalItem(string Name, decimal Total, int Percent);
	public record CategoryOption(int Id, string Name, bool IsIncome);
	public record TransactionItem(int Id, DateTime Date, string CategoryName, bool IsIncome, decimal Amount, string? Note);
	public record FixedExpenseItem(int Id, string CategoryName, decimal Amount, int DayOfMonth, bool IsPosted);
	public record DebtItem(int Id, string Creditor, decimal TotalAmount, decimal RemainingAmount, DateTime? DueDate);
}
