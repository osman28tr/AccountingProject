using AccountingProject.Datas.Repositories.Abstract;
using AccountingProject.Entities;
using AccountingProject.Services.Abstract;
using System.Linq.Expressions;

namespace AccountingProject.Services.Concrete
{
	public class BudgetService : IBudgetService
	{
		private readonly IBudgetRepository _budgetRepository;

		public BudgetService(IBudgetRepository budgetRepository)
		{
			_budgetRepository = budgetRepository;
		}

		public async Task AddAsync(Budget entity)
		{
			await _budgetRepository.AddAsync(entity);
		}

		public async Task AddRangeAsync(List<Budget> entities)
		{
			await _budgetRepository.AddRangeAsync(entities);
		}

		public async Task<IEnumerable<Budget>> FindAsync(Expression<Func<Budget, bool>> predicate)
		{
			return await _budgetRepository.FindAsync(predicate);
		}

		public async Task<IEnumerable<Budget>> GetAllAsync()
		{
			return await _budgetRepository.GetAllAsync();
		}

		public async Task<Budget> GetByIdAsync(int id)
		{
			return await _budgetRepository.GetByIdAsync(id);
		}

		public async Task<Budget> GetByIdAsyncWithIncludes(int id, params Expression<Func<Budget, object>>[] includes)
		{
			return await _budgetRepository.GetByIdAsyncWithIncludes(id, includes);
		}

		public void Remove(Budget entity)
		{
			_budgetRepository.Remove(entity);
		}

		public async Task SaveChangesAsync()
		{
			await _budgetRepository.SaveChangesAsync();
		}

		public void Update(Budget entity)
		{
			_budgetRepository.Update(entity);
		}
	}
}
