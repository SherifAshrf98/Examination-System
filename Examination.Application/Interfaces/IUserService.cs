using Examination.Application.Common;
using Examination.Application.Dtos.AppUser;
using Examination.Application.Dtos.Student;
using Examination.Domain.Entities.Identity;
using Examination.Infrastructure.Repositories;
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
		Task<Result<bool>> ChangeStudentState(string id, ChangeUserStateDto changeUserStateDto);
		Task<Result<StudentWithSubjectsDto>> GetStudentWithSubjects(string id);
		Task<Result<Pagination<StudentDto>>> GetAllStudentsAsync(int pageNumber, int pageSize);
		Task<Result<int>> GetTotalStudentsCountAsync();
		Task<Result<StudentDto>> GetStudentByIdAsync(string id);
		Task<Result<Pagination<ExamHistoryDto>>> GetExamsHistory(string studentId, int pageNumber, int pageSize);
	}
}
