using Examination.Application.Dtos.Subject;
using Examination.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examination.Api.Controllers
{
	[Authorize(Roles = "Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class SubjectController : ControllerBase
	{
		private readonly ISubjectService _subjectService;

		public SubjectController(ISubjectService subjectService)
		{
			_subjectService = subjectService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll(int pageNumber, int PageSize)
		{
			var result = await _subjectService.GetAllSubjectsAsync(pageNumber, PageSize);

			if (!result.IsSuccess)
			{
				return BadRequest(result.Errors);
			}

			return Ok(result.Value);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var result = await _subjectService.GetSubjectByIdAsync(id);

			if (!result.IsSuccess)
			{
				return BadRequest(result.Errors);
			}

			return Ok(result.Value);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateSubject(int id, UpdateSubjectDto updateSubjectDto)
		{
			var result = await _subjectService.UpdateSubjectAsync(id, updateSubjectDto);

			if (!result.IsSuccess)
			{
				return BadRequest(result.Errors);
			}

			return Ok(new { Message = "Subject Updated Successfully" });
		}

		[HttpPost]
		public async Task<IActionResult> AddSubject(CreateSubjectDto createSubjectDto)
		{
			var result = await _subjectService.AddSubjectAsync(createSubjectDto);

			if (!result.IsSuccess)
			{
				return BadRequest(result.Errors);
			}

			return Ok(new { Message = "Subject Added Successfully" });
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSubject(int id)
		{
			var result = await _subjectService.DeleteSubjectAsync(id);

			if (!result.IsSuccess)
			{
				return BadRequest(result.Errors);
			}

			return Ok(new { Message = "Subject Deleted Successfully" });
		}
	}
}
