using AccountingProject.Entities;
using System.Linq.Expressions;

namespace AccountingProject.Services.Abstract
{
	public interface ICategoryService
	{
		Task<Category> GetByIdAsync(int id);
		Task<Category> GetByIdAsyncWithIncludes(int id, params Expression<Func<Category, object>>[] includes);
		Task<IEnumerable<Category>> GetAllAsync();
		Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> predicate);
		Task AddAsync(Category entity);
		Task AddRangeAsync(List<Category> entities);
		Task SaveChangesAsync();
		void Update(Category entity);
		void Remove(Category entity);
	}
}
