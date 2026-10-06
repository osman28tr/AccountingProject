using AccountingProject.Models;

namespace AccountingProject.Services.Abstract
{
	public interface IDashboardService
	{
		Task<DashboardViewModel> GetDashboardAsync(string? month, string? filter);
		Task<bool> AddTransactionAsync(TransactionCreateViewModel model);
		Task<bool> DeleteTransactionAsync(int id);
	}
}
