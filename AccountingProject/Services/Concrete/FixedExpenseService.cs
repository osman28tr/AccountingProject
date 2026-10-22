using AccountingProject.Datas.Repositories.Abstract;
using AccountingProject.Entities;
using AccountingProject.Services.Abstract;
using System.Linq.Expressions;

namespace AccountingProject.Services.Concrete
{
	public class FixedExpenseService : IFixedExpenseService
	{
		private readonly IFixedExpenseRepository _fixedExpenseRepository;

		public FixedExpenseService(IFixedExpenseRepository fixedExpenseRepository)
		{
			_fixedExpenseRepository = fixedExpenseRepository;
		}

		public async Task AddAsync(FixedExpense entity)
		{
			await _fixedExpenseRepository.AddAsync(entity);
		}

		public async Task AddRangeAsync(List<FixedExpense> entities)
		{
			await _fixedExpenseRepository.AddRangeAsync(entities);
		}

		public async Task<IEnumerable<FixedExpense>> FindAsync(Expression<Func<FixedExpense, bool>> predicate)
		{
			return await _fixedExpenseRepository.FindAsync(predicate);
		}

		public async Task<IEnumerable<FixedExpense>> GetAllAsync()
		{
			return await _fixedExpenseRepository.GetAllAsync();
		}

		public async Task<FixedExpense> GetByIdAsync(int id)
		{
			return await _fixedExpenseRepository.GetByIdAsync(id);
		}

		public async Task<FixedExpense> GetByIdAsyncWithIncludes(int id, params Expression<Func<FixedExpense, object>>[] includes)
		{
			return await _fixedExpenseRepository.GetByIdAsyncWithIncludes(id, includes);
		}

		public void Remove(FixedExpense entity)
		{
			_fixedExpenseRepository.Remove(entity);
		}

		public async Task SaveChangesAsync()
		{
			await _fixedExpenseRepository.SaveChangesAsync();
		}

		public void Update(FixedExpense entity)
		{
			_fixedExpenseRepository.Update(entity);
		}
	}
}
