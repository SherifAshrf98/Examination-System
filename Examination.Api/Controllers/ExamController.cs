using Examination.Api.Helpers;
using Examination.Application.Dtos.Exam;
using Examination.Application.Interfaces;
using Examination.Application.Interfaces.Repositories;
using Examination.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Examination.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExamController : ControllerBase
	{
		private readonly IExamService _examService;

		public ExamController(IExamService examService)
		{
			_examService = examService;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetExamById(int id)
		{
			var result = await _examService.GetExamById(id);
			if (!result.IsSuccess)
			{
				if (result.IsNotFound)
					return NotFound(new ApiResponse(404, result.Errors.FirstOrDefault()));

				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors });
			}
			return Ok(new ApiResponse<ExamDto>(200, result.Value));
		}
		[Authorize]
		[HttpPost("request")]
		public async Task<IActionResult> CreateExam(int subjectId)
		{
			var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var result = await _examService.CreateOrCountinueExamAsync(subjectId, studentId);
			if (!result.IsSuccess)
			{
				if (result.IsNotFound)
					return NotFound(new ApiResponse(404, result.Errors.FirstOrDefault()));

				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors });
			}
			return CreatedAtAction(nameof(GetExamById), new
			{
				id = result.Value.Id
			}, new ApiResponse<ExamDto>(201, result.Value));
		}
	}
}
