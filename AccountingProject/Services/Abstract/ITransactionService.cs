using AccountingProject.Entities;
using System.Linq.Expressions;

namespace AccountingProject.Services.Abstract
{
	public interface ITransactionService
	{
		Task<Transaction> GetByIdAsync(int id);
		Task<Transaction> GetByIdAsyncWithIncludes(int id, params Expression<Func<Transaction, object>>[] includes);
		Task<IEnumerable<Transaction>> GetAllAsync();
		Task<IEnumerable<Transaction>> FindAsync(Expression<Func<Transaction, bool>> predicate);
		Task AddAsync(Transaction entity);
		Task AddRangeAsync(List<Transaction> entities);
		Task SaveChangesAsync();
		void Update(Transaction entity);
		void Remove(Transaction entity);
	}
}
