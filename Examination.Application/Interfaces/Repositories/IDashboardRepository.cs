using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Interfaces.Repositories
{
	public interface IDashboardRepository
	{
		Task<int> GetTotalStudentsCount();
		Task<int> GetTotalSubmittedExams();
		Task<int> GetTotalPassedExams();
		Task<int> GetTotalFailedExams();
		Task<int> GetStudentTotalExams(string studentId);
		Task<int> GetStudentSubmittedExams(string studentId);
		Task<int> GetStudentAverageScore(string studentId);
		Task<int> GetStudentTotalSubjects(string studentId);
	}
}
