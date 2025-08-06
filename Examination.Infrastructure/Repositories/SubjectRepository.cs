using Examination.Application.Common;
using Examination.Application.Dtos.Subject;
using Examination.Application.Interfaces.Repositories;
using Examination.Domain.Entities;
using Examination.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Examination.Infrastructure.Repositories
{
	public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
	{
		private readonly AppDbContext _dbcontext;

		public SubjectRepository(AppDbContext context) : base(context)
		{
			_dbcontext = context;
		}
		public async Task<IReadOnlyList<SubjectDto>> GetAllSubjectsAsync()
		{
			return await _dbcontext.Subjects
				.AsNoTracking()
				.OrderBy(s => s.Name)
				.Select(s => new SubjectDto
				{
					id = s.Id,
					Name = s.Name
				}).ToListAsync();

		}
		public async Task<Pagination<SubjectDto>> GetAllSubjectsPaginatedAsync(int pageNumber, int pageSize)
		{

			var Items = await _dbcontext.Subjects.AsNoTracking()
				.OrderBy(s => s.Name)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.Select(s => new SubjectDto
				{
					id = s.Id,
					Name = s.Name
				}).ToListAsync();

			var count = await _dbcontext.Subjects.CountAsync();

			return new Pagination<SubjectDto>
			{
				Items = Items,
				PageNumber = pageNumber,
				PageSize = pageSize,
				PageCount = (int)Math.Ceiling((double)count / pageSize),
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
