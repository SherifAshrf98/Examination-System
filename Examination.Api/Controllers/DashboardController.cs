using Examination.Api.Helpers;
using Examination.Application.Dtos;
using Examination.Application.Interfaces;
using Examination.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examination.Api.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class DashboardController : ControllerBase
	{
		private readonly IDashboardService _dashboardService;

		public DashboardController(IDashboardService dashboardService)
		{
			_dashboardService = dashboardService;
		}

		[HttpGet("summary")]
		public async Task<IActionResult> GetDashboardSummary()
		{
			var result = await _dashboardService.GetDashboardStatsAsync();

			if (!result.IsSuccess)
				return BadRequest(new ApiResponse(400, result.Errors.First()));

			return Ok(new ApiResponse<DashboardStatsDto>(200, result.Value));
		}
	}
}
