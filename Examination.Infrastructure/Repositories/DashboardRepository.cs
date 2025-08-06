using Examination.Application.Interfaces.Repositories;
using Examination.Domain.Entities.Enums;
using Examination.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


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
		public async Task<int> GetStudentTotalExams(string studentId)
		{
			return await _dbContext.Exams.CountAsync(e => e.StudentId == studentId);
		}
		public async Task<int> GetStudentSubmittedExams(string studentId)
		{
			return await _dbContext.Exams.CountAsync(e => e.StudentId == studentId && e.status == ExamStatus.Submitted);
		}
		public async Task<int> GetStudentAverageScore(string studentId)
		{
			return (int)await _dbContext.Submissions.Where(s => s.StudentId == studentId).AverageAsync(s => s.Score);
		}
		public async Task<int> GetStudentTotalSubjects(string studentId)
		{
			return await _dbContext.StudentSubjects.CountAsync(sb => sb.StudentId == studentId);
				
		}
	}
}
