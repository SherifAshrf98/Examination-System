using Examination.Application.Common;
using Examination.Application.Dtos.Subject;
using Examination.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Interfaces.Repositories
{
	public interface ISubjectRepository : IGenericRepository<Subject>
	{
		public Task<IReadOnlyList<SubjectDto>> GetAllSubjectsAsync();
		Task<Subject> GetSubjectByNameAsync(string name);
		Task<Pagination<SubjectDto>> GetAllSubjectsPaginatedAsync(int pageNumber, int pageSize);
		Task<int> GetTotalSubjectsCountAsync();
	}
}
