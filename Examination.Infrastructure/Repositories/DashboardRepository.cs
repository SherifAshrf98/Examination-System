using Examination.Application.Interfaces.Repositories;
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
	public class DashboardRepository : IDashboardRepository
	{
		private readonly AppDbContext _dbContext;

		public DashboardRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<int> GetTotalFailedExams()
		{
			return await _dbContext.Submissions.CountAsync(s => s.Score < 50);

		}

		public async Task<int> GetTotalPassedExams()
		{
			return await _dbContext.Submissions.CountAsync(s => s.Score >= 50);
		}

		public async Task<int> GetTotalStudentsCount()
		{
			var studentRoleId = await _dbContext.Roles
				.Where(r => r.Name == "Student")
				.Select(r => r.Id)
				.FirstOrDefaultAsync();

			var studentCount = await _dbContext.UserRoles
				.Where(ur => ur.RoleId == studentRoleId)
				.CountAsync();

			return studentCount;
		}
		public async Task<int> GetTotalSubmittedExams()
		{
			return await _dbContext.Exams.CountAsync(e => e.status == ExamStatus.Submitted);
		}
	}
}
