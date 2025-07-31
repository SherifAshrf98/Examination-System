using Examination.Application.Interfaces.Repositories;
using Examination.Domain.Entities;
using Examination.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Infrastructure.Repositories
{
	public class ExamSubmisisonRepository : GenericRepository<ExamSubmission>, IExamSubmisisonRepository
	{
		private readonly AppDbContext _dbContext;

		public ExamSubmisisonRepository(AppDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<ExamSubmission> GetByExamIdAndStudentId(int examId, string studentId)
		{
			return await _dbContext.Submissions.FirstOrDefaultAsync(s => s.ExamId == examId && s.StudentId == studentId);
		}
	}
}
