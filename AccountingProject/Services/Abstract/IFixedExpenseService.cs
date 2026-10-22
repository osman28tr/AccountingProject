using AccountingProject.Entities;
using System.Linq.Expressions;

namespace AccountingProject.Services.Abstract
{
	public interface IFixedExpenseService
	{
		Task<FixedExpense> GetByIdAsync(int id);
		Task<FixedExpense> GetByIdAsyncWithIncludes(int id, params Expression<Func<FixedExpense, object>>[] includes);
		Task<IEnumerable<FixedExpense>> GetAllAsync();
		Task<IEnumerable<FixedExpense>> FindAsync(Expression<Func<FixedExpense, bool>> predicate);
		Task AddAsync(FixedExpense entity);
		Task AddRangeAsync(List<FixedExpense> entities);
		Task SaveChangesAsync();
		void Update(FixedExpense entity);
		void Remove(FixedExpense entity);
	}
}
