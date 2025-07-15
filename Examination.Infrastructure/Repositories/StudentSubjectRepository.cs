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
	public class StudentSubjectRepository : GenericRepository<StudentSubject>, IStudentSubjectRepository
	{
		private readonly AppDbContext _dbContext;

		public StudentSubjectRepository(AppDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<StudentSubject> GetByStudentIdAndSubjectIdAsync(string studentId, int subjectId)
		{
			return await _dbContext.StudentSubjects
				.FirstOrDefaultAsync(ss => ss.StudentId == studentId && ss.SubjectId == subjectId);
		}

		public async Task<bool> IsStudentEnrolledInSubjectAsync(string studentId, int subjectId)
		{
			return await _dbContext.StudentSubjects.AnyAsync(ss => ss.SubjectId == subjectId && ss.StudentId == studentId);
		}
	}
}
