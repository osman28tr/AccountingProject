using AccountingProject.Datas.Repositories.Abstract;
using AccountingProject.Entities;
using AccountingProject.Services.Abstract;
using System.Linq.Expressions;

namespace AccountingProject.Services.Concrete
{
	public class DebtService : IDebtService
	{
		private readonly IDebtRepository _debtRepository;

		public DebtService(IDebtRepository debtRepository)
		{
			_debtRepository = debtRepository;
		}

		public async Task AddAsync(Debt entity)
		{
			await _debtRepository.AddAsync(entity);
		}

		public async Task AddRangeAsync(List<Debt> entities)
		{
			await _debtRepository.AddRangeAsync(entities);
		}

		public async Task<IEnumerable<Debt>> FindAsync(Expression<Func<Debt, bool>> predicate)
		{
			return await _debtRepository.FindAsync(predicate);
		}

		public async Task<IEnumerable<Debt>> GetAllAsync()
		{
			return await _debtRepository.GetAllAsync();
		}

		public async Task<Debt> GetByIdAsync(int id)
		{
			return await _debtRepository.GetByIdAsync(id);
		}

		public async Task<Debt> GetByIdAsyncWithIncludes(int id, params Expression<Func<Debt, object>>[] includes)
		{
			return await _debtRepository.GetByIdAsyncWithIncludes(id, includes);
		}

		public void Remove(Debt entity)
		{
			_debtRepository.Remove(entity);
		}

		public async Task SaveChangesAsync()
		{
			await _debtRepository.SaveChangesAsync();
		}

		public void Update(Debt entity)
		{
			_debtRepository.Update(entity);
		}
	}
}
