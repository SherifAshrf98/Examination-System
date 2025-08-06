using Examination.Api.Helpers;
using Examination.Application.Common;
using Examination.Application.Dtos.AppUser;
using Examination.Application.Dtos.Exam;
using Examination.Application.Dtos.ExamSubmission;
using Examination.Application.Interfaces;
using Examination.Application.Interfaces.Repositories;
using Examination.Domain.Entities;
using Examination.Infrastructure.Repositories;
using Examination.Infrastructure.Services;
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
		public async Task<IActionResult> CreateExam([FromBody] ExamRequestDto examRequestDto)
		{
			var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var result = await _examService.CreateOrCountinueExamAsync(examRequestDto.subjectId, studentId);

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

		[HttpPost("submit")]
		public async Task<IActionResult> SubmitExam([FromBody] SubmitExamDto submitExamDto)
		{
			var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var result = await _examService.SubmitExam(studentId, submitExamDto);

			if (!result.IsSuccess)
			{
				if (result.IsNotFound)
					return NotFound(new ApiResponse(404, result.Errors.FirstOrDefault()));

				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors });
			}

			return Ok(new ApiResponse(200, "Exam Submitted successfully"));
		}

		[HttpGet("history")]
		public async Task<IActionResult> GetExamHistory(int pageNumber, int pageSize)
		{

			if (pageNumber <= 0)
				return BadRequest(new ApiValidationErrorResponse() { Errors = new List<string> { "Subject ID must be greater than 0" } });


			if (pageSize <= 0 || pageSize > 100)
				return BadRequest(new ApiValidationErrorResponse() { Errors = new List<string> { "Page size must be between 1 and 50" } });

			var result = await _examService.GetExamHistoryAsync(pageNumber, pageSize);

			return Ok(new ApiResponse<Pagination<ExamHistoryDto>>(200, result.Value));
		}

		[HttpGet("result/{examId}")]
		public async Task<IActionResult> GetExamQuestionResult(int examId)
		{
			var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (string.IsNullOrWhiteSpace(studentId))
			{
				return BadRequest(new ApiValidationErrorResponse() { Errors = new List<string> { "Invalid student ID." } });
			}

			var result = await _examService.GetExamQuestionResultAsync(examId, studentId);

			if (!result.IsSuccess)
			{
				if (result.IsNotFound)
					return NotFound(new ApiResponse(404, result.Errors.FirstOrDefault()));

				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors });
			}

			return Ok(new ApiResponse<List<ExamResultDto>>(200, result.Value));
		}
	}
}
