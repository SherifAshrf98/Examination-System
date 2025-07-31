

using Examination.Application.Common;
using Examination.Application.Dtos.Exam;
using Examination.Application.Interfaces.Repositories;
using Examination.Domain.Entities;
using Examination.Domain.Entities.Enums;
using Examination.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Infrastructure.Repositories
{
	public class ExamsRepository : GenericRepository<Exam>, IExamRepository
	{
		private readonly AppDbContext _dbContext;

		public ExamsRepository(AppDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<ExamDto> GetExamByIdAsync(int id)
		{
			return await _dbContext.Exams
				.Where(e => e.Id == id)
				.Select(e => new ExamDto
				{
					Id = e.Id,
					SubjectName = e.Subject.Name,
					Duration = e.Duration,
					StartedAt = e.StartedAt,
					RemainingTime = e.Duration * 60,
					Questions = e.ExamQuestions.Select(eq => new ExamQuestionDto
					{
						questionId = eq.Question.Id,
						text = eq.Question.Text,
						Options = eq.Question.Options.Select(o => new ExamQuestionOptionDto
						{
							Id = o.Id,
							Text = o.Text,
						}).ToList()
					}).ToList()
				}).FirstOrDefaultAsync();
		}

		public async Task<Exam?> GetInProgressExam(string studentId, int subjectId)
		{
			return await _dbContext.Exams
				.Include(e => e.Subject)
				.Include(e => e.ExamQuestions)
					.ThenInclude(eq => eq.Question)
						.ThenInclude(q => q.Options)
				.FirstOrDefaultAsync(e => e.StudentId == studentId && e.SubjectId == subjectId && e.status == ExamStatus.InProgress);
		}

		public async Task<Pagination<ExamHistoryDto>> GetExamHistory(int pageNumber, int pageSize)
		{
			var PaginatedList = await _dbContext.Exams
				.OrderByDescending(e => e.StartedAt)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize).Select(e => new ExamHistoryDto
				{
					Duration = e.Duration,
					StartedAt = e.StartedAt,
					StudentName = $"{e.Student.FirstName}{e.Student.LastName}",
					SubjectName = e.Subject.Name,
					Score = e.Submission.Score ?? 60
				}).ToListAsync();

			var totalCount = await _dbContext.Exams.CountAsync();

			return new Pagination<ExamHistoryDto>
			{
				Items = PaginatedList,
				PageNumber = pageNumber,
				PageSize = pageSize,
				PageCount = (int)Math.Ceiling((double)totalCount / pageSize),
				TotalCount = totalCount
			};
		}

		public async Task<Pagination<ExamHistoryDto>> GetExamHistoryByStudentId(string studentId, int pageNumber, int pageSize)
		{
			var PaginatedList = await _dbContext.Exams
				.Where(e => e.StudentId == studentId)
				.OrderBy(e => e.Subject.Name)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize).Select(e => new ExamHistoryDto
				{
					Duration = e.Duration,
					StartedAt = e.StartedAt,
					StudentName = $"{e.Student.FirstName}{e.Student.LastName}",
					SubjectName = e.Subject.Name,
					Score = e.Submission.Score ?? 60
				}).ToListAsync();

			var totalCount = await _dbContext.Exams.CountAsync(e => e.StudentId == studentId);

			return new Pagination<ExamHistoryDto>
			{
				Items = PaginatedList,
				PageNumber = pageNumber,
				PageSize = pageSize,
				PageCount = (int)Math.Ceiling((double)totalCount / pageSize),
				TotalCount = totalCount
			};
		}

		public async Task<Exam?> GetExamWithQuestionsAndAnswers(int id)
		{
			return await _dbContext.Exams
				.Include(e => e.Subject)
				.Include(e => e.ExamQuestions)
					.ThenInclude(eq => eq.Question)
						.ThenInclude(q => q.Options)
						.FirstOrDefaultAsync(e => e.Id == id);
		}
	}
}
