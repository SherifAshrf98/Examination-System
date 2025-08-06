using Examination.Application.Common;
using Examination.Application.Dtos.Dashboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Interfaces
{
	public interface IDashboardService
	{
		Task<Result<AdminDashboardStatsDto>> GetAdminDashboardStatsAsync();

		Task<Result<StudentDashboardStatsDto>> GetStudentDashboardStatsAsync(string studentId);
	}
}
