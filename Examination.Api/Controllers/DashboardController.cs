using Examination.Api.Helpers;
using Examination.Application.Dtos.Dashboards;
using Examination.Application.Interfaces;
using Examination.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

		[Authorize(Roles = "Admin")]
		[HttpGet("Admin/summary")]
		public async Task<IActionResult> GetAdminDashboardSummary()
		{
			var result = await _dashboardService.GetAdminDashboardStatsAsync();

			if (!result.IsSuccess)
				return BadRequest(new ApiResponse(400, result.Errors.First()));

			return Ok(new ApiResponse<AdminDashboardStatsDto>(200, result.Value));
		}

		[Authorize(Roles = "Student")]
		[HttpGet("Student/summary")]
		public async Task<IActionResult> GetStudentDashboardSummary()

		{
			var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _dashboardService.GetStudentDashboardStatsAsync(studentId);

			if (!result.IsSuccess)
				return BadRequest(new ApiResponse(400, result.Errors.First()));

			return Ok(new ApiResponse<StudentDashboardStatsDto>(200, result.Value));
		}

	}
}
