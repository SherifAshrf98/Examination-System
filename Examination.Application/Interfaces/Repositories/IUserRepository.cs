using Examination.Application.Common;
using Examination.Application.Dtos.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Infrastructure.Repositories
{
	public interface IUserRepository
	{
		Task<Pagination<StudentDto>> GetAllStudentsAsync(int pageNumber, int pageSize);
		Task<StudentWithSubjectsDto> GetStudentWithSubjects(string id);
		Task<int> GetStudentsCountAsync(string id);
	}
}
