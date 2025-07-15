using Examination.Api.Helpers;
using Examination.Application.Dtos.Question;
using Examination.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examination.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuestionController : ControllerBase
	{
		private readonly IQuestionService _questionService;

		public QuestionController(IQuestionService questionService)
		{
			_questionService = questionService;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetQuestionById(int id)
		{
			var result = await _questionService.GetQuestionByIdAsync(id);

			if (!result.IsSuccess)
			{
				if (result.IsNotFound)
					return NotFound(new ApiResponse(404, result.Errors.FirstOrDefault()));

				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors });
			}
			return Ok(new ApiResponse<QuestionDto>(200, result.Value));
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionDto createQuestionDto)
		{
			var result = await _questionService.CreateQuestionAsync(createQuestionDto);

			if (!result.IsSuccess)
			{
				if (result.IsNotFound)
					return NotFound(new ApiResponse(404, result.Errors.FirstOrDefault()));

				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors });
			}

			return CreatedAtAction(nameof(GetQuestionById), new { id = result.Value }, new ApiResponse<int>(201, result.Value, "Question created successfully"));
		}
	}
}
