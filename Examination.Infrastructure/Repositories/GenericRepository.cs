using Examination.Application.Interfaces.Repositories;
using Examination.Domain.Entities;
using Examination.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Infrastructure.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly AppDbContext _dbContext;

		public GenericRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync();
		}

		public async Task AddAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
		}

		public void Delete(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
		}

		public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate)
		{
			return await _dbContext.Set<T>().Where(predicate).ToListAsync();
		}
		public async Task<T> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public async Task<bool> IsExistingAsync(Expression<Func<T, bool>> predicate)
		{
			return await _dbContext.Set<T>().AnyAsync(predicate);
		}

		public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
		{
			return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
		}
	}
}
