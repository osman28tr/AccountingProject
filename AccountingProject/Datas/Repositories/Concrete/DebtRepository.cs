using AccountingProject.Datas.Contexts;
using AccountingProject.Datas.Repositories.Abstract;
using AccountingProject.Entities;

namespace AccountingProject.Datas.Repositories.Concrete
{
	public class DebtRepository : GenericRepository<Debt>, IDebtRepository
	{
		private readonly AppDbContex _context;
		public DebtRepository(AppDbContex context) : base(context)
		{
			_context = context;
		}
	}
}
