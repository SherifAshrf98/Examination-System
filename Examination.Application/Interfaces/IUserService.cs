using Examination.Application.Common;
using Examination.Application.Dtos.AppUser;
using Examination.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Interfaces
{
	public interface IUserService
	{
		Task<Result<StudentWithSubjectsDto>> GetStudentWithSubjects(string id);
		Task<Result<Pagination<StudentDto>>> GetAllStudentsAsync(int pageNumber, int pageSize);
		Task<Result<int>> GetTotalStudentsCountAsync();
		Task<Result<StudentDto>> GetStudentByIdAsync(string id);
	}
}
