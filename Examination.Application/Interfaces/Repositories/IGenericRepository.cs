﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Interfaces.Repositories
{
	public interface IGenericRepository<T> where T : class
	{
		Task<T> GetByIdAsync(int id);
		Task<IReadOnlyList<T>> GetAllAsync();
		Task AddAsync(T entity);
		void Delete(T entity);
		Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
		Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);
		Task<bool> IsExistingAsync(Expression<Func<T, bool>> predicate);
	}
}
