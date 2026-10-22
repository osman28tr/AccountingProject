using AccountingProject.Entities;
using System.Linq.Expressions;

namespace AccountingProject.Services.Abstract
{
	public interface IBudgetService
	{
		Task<Budget> GetByIdAsync(int id);
		Task<Budget> GetByIdAsyncWithIncludes(int id, params Expression<Func<Budget, object>>[] includes);
		Task<IEnumerable<Budget>> GetAllAsync();
		Task<IEnumerable<Budget>> FindAsync(Expression<Func<Budget, bool>> predicate);
		Task AddAsync(Budget entity);
		Task AddRangeAsync(List<Budget> entities);
		Task SaveChangesAsync();
		void Update(Budget entity);
		void Remove(Budget entity);
	}
}
