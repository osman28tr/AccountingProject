using System.ComponentModel.DataAnnotations;

namespace AccountingProject.Models
{
	public class DashboardViewModel
	{
		public string Month { get; set; } = "";              // yyyy-MM
		public string Filter { get; set; } = "hepsi";        // hepsi | gelir | gider

		// Seçili ay özeti
		public decimal TotalIncome { get; set; }
		public decimal TotalExpense { get; set; }
		public decimal Net => TotalIncome - TotalExpense;
		public decimal? IncomeChange { get; set; }           // Geçen aya göre % (geçen ay 0 ise null)
		public decimal? ExpenseChange { get; set; }

		// Son 12 ay ortalamaları (kaydı olan aylar üzerinden)
		public int ActiveMonthCount { get; set; }
		public decimal AvgIncome { get; set; }
		public decimal AvgExpense { get; set; }
		public decimal AvgNet => AvgIncome - AvgExpense;

		public decimal? BiggestAmount { get; set; }
		public string? BiggestCategory { get; set; }
		public string? BiggestNote { get; set; }

		public List<MonthlyTrendItem> Trend { get; set; } = new();
		public List<CategoryTotalItem> ExpenseByCategory { get; set; } = new();
		public List<TransactionListItem> Transactions { get; set; } = new();
		public List<CategoryOption> Categories { get; set; } = new();
	}

	public class MonthlyTrendItem
	{
		public string Label { get; set; } = "";
		public decimal Income { get; set; }
		public decimal Expense { get; set; }
		public decimal Net { get; set; }
		public bool HasData { get; set; }
	}

	public class CategoryTotalItem
	{
		public string Name { get; set; } = "";
		public decimal Total { get; set; }
		public int Percent { get; set; }
	}

	public class TransactionListItem
	{
		public long Id { get; set; }
		public DateTime Date { get; set; }
		public string CategoryName { get; set; } = "";
		public bool IsIncome { get; set; }
		public decimal Amount { get; set; }
		public string? Note { get; set; }
	}

	public class CategoryOption
	{
		public int Id { get; set; }
		public string Name { get; set; } = "";
		public string Type { get; set; } = "";               // gelir | gider
	}

	public class TransactionCreateViewModel
	{
		[Range(1, int.MaxValue, ErrorMessage = "Kategori seçin.")]
		public int CategoryId { get; set; }

		[Range(0.01, 999999999, ErrorMessage = "0'dan büyük bir tutar girin.")]
		public decimal Amount { get; set; }

		[Required(ErrorMessage = "Tarih seçin.")]
		public DateTime? Date { get; set; }

		[StringLength(80)]
		public string? Note { get; set; }
	}
}
