using AccountingProject.Entities;
using System.Linq.Expressions;

namespace AccountingProject.Services.Abstract
{
	public interface IDebtService
	{
		Task<Debt> GetByIdAsync(int id);
		Task<Debt> GetByIdAsyncWithIncludes(int id, params Expression<Func<Debt, object>>[] includes);
		Task<IEnumerable<Debt>> GetAllAsync();
		Task<IEnumerable<Debt>> FindAsync(Expression<Func<Debt, bool>> predicate);
		Task AddAsync(Debt entity);
		Task AddRangeAsync(List<Debt> entities);
		Task SaveChangesAsync();
		void Update(Debt entity);
		void Remove(Debt entity);
	}
}
