using Examination.Application.Common;
using Examination.Application.Dtos;
using Examination.Application.Dtos.Subject;
using Examination.Application.Interfaces.Repositories;
using Examination.Domain.Entities;
using Examination.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Infrastructure.Repositories
{
	public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
	{
		private readonly AppDbContext _dbcontext;

		public SubjectRepository(AppDbContext context) : base(context)
		{
			_dbcontext = context;
		}

		public async Task<Pagination<SubjectDto>> GetAllSubjectsAsync(int pageNumber, int pageSize)
		{

			var Items = await _dbcontext.Subjects
				.OrderBy(s => s.Name)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.Select(s => new SubjectDto
				{
					id = s.Id,
					Name = s.Name
				}).ToListAsync();

			var count = await GetTotalSubjectsCountAsync();

			return new Pagination<SubjectDto>
			{
				Items = Items,
				PageNumber = pageNumber,
				PageSize = pageSize,
				TotalCount = count
			};
		}

		public async Task<Subject> GetSubjectByNameAsync(string name)
		{
			return await _dbcontext.Subjects
				.Where(s => s.Name.ToLower() == name.ToLower())
				.FirstOrDefaultAsync();
		}

		public Task<int> GetTotalSubjectsCountAsync()
		{
			return _dbcontext.Subjects.CountAsync();
		}
	}
}
