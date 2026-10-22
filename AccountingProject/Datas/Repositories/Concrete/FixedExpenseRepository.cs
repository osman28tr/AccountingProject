using AccountingProject.Datas.Contexts;
using AccountingProject.Datas.Repositories.Abstract;
using AccountingProject.Entities;

namespace AccountingProject.Datas.Repositories.Concrete
{
	public class FixedExpenseRepository : GenericRepository<FixedExpense>, IFixedExpenseRepository
	{
		private readonly AppDbContex _context;
		public FixedExpenseRepository(AppDbContex context) : base(context)
		{
			_context = context;
		}
	}
}
