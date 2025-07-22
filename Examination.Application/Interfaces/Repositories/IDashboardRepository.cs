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
	}
}
