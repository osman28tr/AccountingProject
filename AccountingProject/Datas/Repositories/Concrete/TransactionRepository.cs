using AccountingProject.Datas.Contexts;
using AccountingProject.Datas.Repositories.Abstract;
using AccountingProject.Entities;

namespace AccountingProject.Datas.Repositories.Concrete
{
	public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
	{
		private readonly AppDbContex _context;
		public TransactionRepository(AppDbContex context) : base(context)
		{
			_context = context;
		}
	}
}
