using Examination.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examination.Api.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class DashboardController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;

		public DashboardController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		//[HttpGet]
		//public async Task<IActionResult> GetDashboardData()
		//{
		//	var totalQuestions = await _unitOfWork.QuestionsRepository.();
		//	var totalExams = await _unitOfWork.ExamsRepository.;
		//	var totalUsers = await _unitOfWork.UserRepository.CountAsync();
		//	var dashboardData = new
		//	{
		//		TotalQuestions = totalQuestions,
		//		TotalExams = totalExams,
		//		TotalUsers = totalUsers
		//	};
		//	return Ok(new { StatusCode = 200, Data = dashboardData });
		//}


	}
}
