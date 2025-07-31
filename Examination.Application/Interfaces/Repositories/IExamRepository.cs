using Examination.Application.Common;
using Examination.Application.Dtos.Exam;
using Examination.Domain.Entities;
using Examination.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Interfaces.Repositories
{
	public interface IExamRepository : IGenericRepository<Exam>
	{
		Task<Pagination<ExamHistoryDto>> GetExamHistory(int pageNumber, int pageSize);
		Task<Pagination<ExamHistoryDto>> GetExamHistoryByStudentId(string studentId, int pageNumber, int pageSize);
		Task<Exam?> GetInProgressExam(string studentId, int subjectId);
		Task<ExamDto> GetExamByIdAsync(int id);
		Task<Exam?> GetExamWithQuestionsAndAnswers(int id);
	}
}
