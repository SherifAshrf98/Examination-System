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

		public async Task<Exam?> GetInProgressExamById(string studentId, int subjectId)
		{
			return await _dbContext.Exams
				.Include(e => e.Subject)
				.Include(e => e.ExamQuestions)
					.ThenInclude(eq => eq.Question)
						.ThenInclude(q => q.Options)
				.FirstOrDefaultAsync(e => e.StudentId == studentId && e.SubjectId == subjectId && e.status == ExamStatus.InProgress);
		}
	}
}
