using AccountingProject.Entities;
using System.Linq.Expressions;
using System.Security.Principal;

namespace AccountingProject.Datas.Repositories.Abstract
{
	public interface IGenericRepository<T> where T : class,IEntity, new()
	{
		Task<T> GetByIdAsync(int id);
		Task<T> GetByIdAsyncWithIncludes(int id, params Expression<Func<T, object>>[] includes);
		Task<IEnumerable<T>> GetAllAsync();
		Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
		Task AddAsync(T entity);
		Task AddRangeAsync(List<T> entities);
		Task SaveChangesAsync();
		void Update(T entity);
		void Remove(T entity);
	}
}
