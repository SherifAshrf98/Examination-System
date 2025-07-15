using Examination.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Interfaces.Repositories
{
	public interface IStudentSubjectRepository : IGenericRepository<StudentSubject>
	{
		Task<bool> IsStudentEnrolledInSubjectAsync(string studentId, int subjectId);
		Task<StudentSubject> GetByStudentIdAndSubjectIdAsync(string studentId, int subjectId);
	}
}
