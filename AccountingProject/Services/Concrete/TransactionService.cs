using AccountingProject.Datas.Repositories.Abstract;
using AccountingProject.Entities;
using AccountingProject.Services.Abstract;
using System.Linq.Expressions;

namespace AccountingProject.Services.Concrete
{
	public class TransactionService : ITransactionService
	{
		private readonly ITransactionRepository _transactionRepository;

		public TransactionService(ITransactionRepository transactionRepository)
		{
			_transactionRepository = transactionRepository;
		}

		public async Task AddAsync(Transaction entity)
		{
			await _transactionRepository.AddAsync(entity);
		}

		public async Task AddRangeAsync(List<Transaction> entities)
		{
			await _transactionRepository.AddRangeAsync(entities);
		}

		public async Task<IEnumerable<Transaction>> FindAsync(Expression<Func<Transaction, bool>> predicate)
		{
			return await _transactionRepository.FindAsync(predicate);
		}

		public async Task<IEnumerable<Transaction>> GetAllAsync()
		{
			return await _transactionRepository.GetAllAsync();
		}

		public async Task<Transaction> GetByIdAsync(int id)
		{
			return await _transactionRepository.GetByIdAsync(id);
		}

		public async Task<Transaction> GetByIdAsyncWithIncludes(int id, params Expression<Func<Transaction, object>>[] includes)
		{
			return await _transactionRepository.GetByIdAsyncWithIncludes(id, includes);
		}

		public void Remove(Transaction entity)
		{
			_transactionRepository.Remove(entity);
		}

		public async Task SaveChangesAsync()
		{
			await _transactionRepository.SaveChangesAsync();
		}

		public void Update(Transaction entity)
		{
			_transactionRepository.Update(entity);
		}
	}
}
