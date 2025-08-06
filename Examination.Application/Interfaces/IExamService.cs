using Examination.Application.Common;
using Examination.Application.Dtos.Exam;
using Examination.Application.Dtos.ExamSubmission;
using Examination.Infrastructure.Repositories;
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
		Task<Result<bool>> SubmitExam(string studentId, SubmitExamDto submitExamDto);
		Task<Result<Pagination<ExamHistoryDto>>> GetExamHistoryAsync(int pageNumber, int pageSize);
		Task<Result<ExamDto>> GetExamById(int id);
		Task<Result<List<ExamResultDto>>> GetExamQuestionResultAsync(int examId, string studentId);
	}
}
