using Examination.Application.Common;
using Examination.Application.Dtos;
using Examination.Application.Dtos.Subject;
using Examination.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Interfaces
{
	public interface ISubjectService
	{
		Task<Result<Pagination<SubjectDto>>> GetAllSubjectsAsync(int pageNumber, int pageSize);
		Task<Result<SubjectDto>> GetSubjectByIdAsync(int id);
		Task<Result<bool>> AddSubjectAsync(CreateSubjectDto subject);
		Task<Result<bool>> UpdateSubjectAsync(int id, UpdateSubjectDto subject);
		Task<Result<bool>> DeleteSubjectAsync(int id);
	}
}
