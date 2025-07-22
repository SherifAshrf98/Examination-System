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


	
		//}
	}
}
