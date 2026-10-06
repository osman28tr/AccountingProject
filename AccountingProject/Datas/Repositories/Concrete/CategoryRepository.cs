using AccountingProject.Datas.Contexts;
using AccountingProject.Datas.Repositories.Abstract;
using AccountingProject.Entities;

namespace AccountingProject.Datas.Repositories.Concrete
{
	public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
	{
		private readonly AppDbContex _context;
		public CategoryRepository(AppDbContex context) : base(context)
		{
			_context = context;
		}
	}
}
