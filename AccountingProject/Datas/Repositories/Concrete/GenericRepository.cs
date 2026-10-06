using AccountingProject.Datas.Contexts;
using AccountingProject.Datas.Repositories.Abstract;
using AccountingProject.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Principal;

namespace AccountingProject.Datas.Repositories.Concrete
{
	public abstract class GenericRepository<T> : IGenericRepository<T> where T : class,IEntity, new()
	{
		private readonly AppDbContex _context;
		public GenericRepository(AppDbContex context)
		{
			_context = context;
		}
		public async Task AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			await SaveChangesAsync();
		}
		public async Task AddRangeAsync(List<T> entities)
		{
			await _context.Set<T>().AddRangeAsync(entities);
			await SaveChangesAsync();
		}
		public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
		{
			var result = await _context.Set<T>().Where(predicate).ToListAsync();
			return result;
		}
		public async Task<IEnumerable<T>> GetAllAsync()
		{
			var result = await _context.Set<T>().ToListAsync();
			return result;
		}
		public async Task<T> GetByIdAsync(int id)
		{
			var result = await _context.Set<T>().FindAsync(id);
			return result;
		}
		public void Remove(T entity)
		{
			_context.Set<T>().Remove(entity);
			SaveChanges();
		}
		public void Update(T entity)
		{
			try
			{
				_context.Set<T>().Update(entity);
				SaveChanges();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync(); try
			{

			}
			catch (Exception ex)
			{

				throw;
			}
		}
		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public Task<T> GetByIdAsyncWithIncludes(int id, params Expression<Func<T, object>>[] includes)
		{
			var query = _context.Set<T>().AsQueryable();
			foreach (var include in includes)
			{
				query = query.Include(include);
			}
			return query.FirstOrDefaultAsync(e => e.Id == id);
		}
	}
}
