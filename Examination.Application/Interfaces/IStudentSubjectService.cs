using Examination.Application.Common;
using Examination.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Interfaces
{
	public interface IStudentSubjectService
	{
		Task<Result<bool>> EnrollStudentInSubjectAsync(string studentId, int subjectId);

		Task<Result<bool>> UnenrollStudentFromSubjectAsync(string studentId, int subjectId);
	}
}
