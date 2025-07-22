using Examination.Application.Common;
using Examination.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Interfaces
{
	public interface IDashboardService
	{
		//Task<Result<int>> GetTotalStudentsAsync();
		//Task<Result<int>> GetTotalPassedExamsAsync();
		//Task<Result<int>> GetTotalSubmittedExamsAsync();
		//Task<Result<int>> GetTotalFailedExamsAsync();
		Task<Result<DashboardStatsDto>> GetDashboardStatsAsync();
	}
}
