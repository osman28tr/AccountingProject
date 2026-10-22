using AccountingProject.Models;

namespace AccountingProject.Services.Abstract
{
	public interface IDashboardService
	{

		Task<DashboardViewModel> GetDashboardAsync(string? month, string? filter);
		Task<bool> AddTransactionAsync(int categoryId, decimal amount, DateTime date, string? note);
		Task<bool> DeleteTransactionAsync(int id);
		Task SetPoolAsync(decimal amount);
		Task<bool> AddFixedExpenseAsync(decimal amount, int categoryId, int dayOfMonth);
		Task<bool> DeleteFixedExpenseAsync(int id);
		Task<int> PostFixedExpensesAsync(string? month);
		Task<bool> AddDebtAsync(string creditor, decimal totalAmount, DateTime? dueDate);
		Task<bool> PayDebtAsync(int debtId, decimal amount);
		Task<bool> DeleteDebtAsync(int id);
	}
}
