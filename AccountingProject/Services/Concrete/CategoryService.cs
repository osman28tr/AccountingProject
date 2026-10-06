using AccountingProject.Datas.Repositories.Abstract;
using AccountingProject.Entities;
using AccountingProject.Services.Abstract;
using System.Linq.Expressions;

namespace AccountingProject.Services.Concrete
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryService(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public async Task AddAsync(Category entity)
		{
			await _categoryRepository.AddAsync(entity);
		}

		public async Task AddRangeAsync(List<Category> entities)
		{
			await _categoryRepository.AddRangeAsync(entities);
		}

		public async Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> predicate)
		{
			var categories = await _categoryRepository.FindAsync(predicate);
			return categories;
		}

		public async Task<IEnumerable<Category>> GetAllAsync()
		{
			return await _categoryRepository.GetAllAsync();
		}

		public async Task<Category> GetByIdAsync(int id)
		{
			var category = await _categoryRepository.GetByIdAsync(id);
			return category;
		}

		public Task<Category> GetByIdAsyncWithIncludes(int id, params Expression<Func<Category, object>>[] includes)
		{
			throw new NotImplementedException();
		}

		public void Remove(Category entity)
		{
			_categoryRepository.Remove(entity);
		}

		public async Task SaveChangesAsync()
		{
			await _categoryRepository.SaveChangesAsync();
		}

		public void Update(Category entity)
		{
			_categoryRepository.Update(entity);
		}
	}
}
