using AccountingProject.Datas.Contexts;
using AccountingProject.Datas.Repositories.Abstract;
using AccountingProject.Entities;

namespace AccountingProject.Datas.Repositories.Concrete
{
	public class BudgetRepository : GenericRepository<Budget>, IBudgetRepository
	{
		private readonly AppDbContex _context;
		public BudgetRepository(AppDbContex context) : base(context)
		{
			_context = context;
		}
	}
}
