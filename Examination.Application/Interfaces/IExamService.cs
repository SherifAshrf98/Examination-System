using Examination.Application.Common;
using Examination.Application.Dtos.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Interfaces
{
	public interface IExamService
	{
		Task<Result<ExamDto>> CreateOrCountinueExamAsync(int subjectId, string studentId);
		Task<Result<ExamDto>> GetExamById(int id);
	}
}
