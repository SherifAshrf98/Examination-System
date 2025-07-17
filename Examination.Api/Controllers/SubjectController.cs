using Examination.Api.Helpers;
using Examination.Application.Common;
using Examination.Application.Dtos.AppUser;
using Examination.Application.Dtos.ExamConfigurations;
using Examination.Application.Dtos.Subject;
using Examination.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examination.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubjectController : ControllerBase
	{
		private readonly ISubjectService _subjectService;
		private readonly IExamConfigurationsService _examConfigurationsService;

		public SubjectController(ISubjectService subjectService, IExamConfigurationsService examConfigurationsService)
		{
			_subjectService = subjectService;

			_examConfigurationsService = examConfigurationsService;
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAll()
		{
			var result = await _subjectService.GetAllSubjectsAsync();

			if (!result.IsSuccess)
			{
				if (result.IsNotFound)
					return NotFound(new ApiResponse(404, result.Errors.FirstOrDefault()));

				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors });
			}
			return Ok(new ApiResponse<IReadOnlyList<SubjectDto>>(200, result.Value));
		}

		[HttpGet]
		public async Task<IActionResult> GetAllSubjectsWithPagination(int pageNumber, int PageSize)
		{
			var result = await _subjectService.GetAllSubjectsPaginatedAsync(pageNumber, PageSize);


			return Ok(new ApiResponse<Pagination<SubjectDto>>(200, result.Value));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var result = await _subjectService.GetSubjectByIdAsync(id);

			if (!result.IsSuccess)
			{
				if (result.IsNotFound)
					return NotFound(new ApiResponse(404, result.Errors.FirstOrDefault()));

				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors });
			}

			return Ok(new ApiResponse<SubjectDto>(200, result.Value));
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateSubject(int id, UpdateSubjectDto updateSubjectDto)
		{
			var result = await _subjectService.UpdateSubjectAsync(id, updateSubjectDto);

			if (!result.IsSuccess)
			{
				if (result.IsNotFound)
					return NotFound(new ApiResponse(404, result.Errors.FirstOrDefault()));

				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors });
			}

			return Ok(new ApiResponse(204, "Subject Updated Successfully"));
		}

		[HttpPost]
		public async Task<IActionResult> AddSubject(CreateSubjectDto createSubjectDto)
		{
			var result = await _subjectService.AddSubjectAsync(createSubjectDto);

			if (!result.IsSuccess)
			{
				if (result.IsNotFound)
					return NotFound(new ApiResponse(404, result.Errors.FirstOrDefault()));

				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors });
			}

			return Ok(new ApiResponse(201, "Subject Created Succesfully"));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSubject(int id)
		{
			var result = await _subjectService.DeleteSubjectAsync(id);

			if (!result.IsSuccess)
			{
				if (result.IsNotFound)
					return NotFound(new ApiResponse(404, result.Errors.FirstOrDefault()));

				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors });
			}

			return Ok(new ApiResponse(204, "Subject deleted Successfully"));
		}

		[HttpGet("{id}/examConfigs")]
		public async Task<IActionResult> GetExamConfigsBySubjectId(int id)
		{
			var result = await _examConfigurationsService.GetConfigurationsBySubjectIdAsync(id);

			if (!result.IsSuccess)
			{
				if (result.IsNotFound)
					return NotFound(new ApiResponse(404, result.Errors.FirstOrDefault()));

				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors });
			}
			return Ok(new ApiResponse<ExamConfigurationsDto>(200, result.Value));
		}

		[HttpPost("{id}/examConfigs")]
		public async Task<IActionResult> AddExamConfigurations(int id, [FromBody] CreateExamConfigurationsDto createExamConfigurationsDto)
		{
			var result = await _examConfigurationsService.AddExamConfigurations(id, createExamConfigurationsDto);

			if (!result.IsSuccess)
			{
				if (result.IsNotFound)
					return NotFound(new ApiResponse(404, result.Errors.FirstOrDefault()));

				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors });
			}
			return CreatedAtAction(nameof(GetExamConfigsBySubjectId), new { id = result.Value }, new ApiResponse<int>(201, result.Value, "Configurations For this Subject created successfully"));
		}

		[HttpPut("{id}/examConfigs")]
		public async Task<IActionResult> UpdateExamConfigurations(int id, [FromBody] UpdateExamConfigurationsDto updateExamConfigurationsDto)
		{
			var result = await _examConfigurationsService.UpdateExamConfigurations(id, updateExamConfigurationsDto);

			if (!result.IsSuccess)
			{
				if (result.IsNotFound)
					return NotFound(new ApiResponse(404, result.Errors.FirstOrDefault()));

				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors });
			}
			return Ok(new ApiResponse(200, "Exam Configurations Updated Successfully"));
		}
	}
}
