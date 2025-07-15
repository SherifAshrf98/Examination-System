using Examination.Api.Helpers;
using Examination.Application.Common;
using Examination.Application.Dtos.AppUser;
using Examination.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Examination.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		private readonly IUserService _userServices;
		private readonly IStudentSubjectService _studentSubjectService;

		public StudentController(IUserService userServices, IStudentSubjectService studentSubjectService)
		{
			_userServices = userServices;
			_studentSubjectService = studentSubjectService;
		}

		[HttpPost("Subjects/{subjectId}/enroll")]
		public async Task<IActionResult> EnrollToSubject(int subjectId)
		{
			var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (subjectId <= 0)
				return BadRequest(new ApiValidationErrorResponse() { Errors = new List<string> { "Subject ID must be greater than 0" } });

			var result = await _studentSubjectService.EnrollStudentInSubjectAsync(studentId, subjectId);

			if (!result.IsSuccess)
			{
				if (result.IsNotFound)
					return NotFound(new ApiResponse(404, result.Errors.FirstOrDefault()));

				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors });
			}

			return Ok(new ApiResponse(200, "Student enrolled successfully"));
		}

		[HttpGet]
		public async Task<IActionResult> GetAllStudents(int pageNumber, int pageSize)
		{
			if (pageNumber <= 0)
				return BadRequest(new ApiValidationErrorResponse() { Errors = new List<string> { "Subject ID must be greater than 0" } });


			if (pageSize <= 0 || pageSize > 100)
				return BadRequest(new ApiValidationErrorResponse() { Errors = new List<string> { "Page size must be between 1 and 50" } });

			var result = await _userServices.GetAllStudentsAsync(pageNumber, pageSize);

			return Ok(new ApiResponse<Pagination<StudentDto>>(200, result.Value));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetStudentById(string id)
		{
			var result = await _userServices.GetStudentByIdAsync(id);

			if (!result.IsSuccess)
			{
				if (result.IsNotFound)
					return NotFound(new ApiResponse(404, result.Errors.FirstOrDefault()));

				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors });
			}

			return Ok(new ApiResponse<StudentDto>(200, result.Value));
		}

		[Authorize(Roles = "Student")]
		[HttpGet("{id}/Subjects")]
		public async Task<IActionResult> GetStudentWithSubjects(string id)
		{
			var result = await _userServices.GetStudentWithSubjects(id);

			if (!result.IsSuccess)
			{
				if (result.IsNotFound)
					return NotFound(new ApiResponse(404, result.Errors.FirstOrDefault()));

				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors });
			}
			return Ok(new ApiResponse<StudentWithSubjectsDto>(200, result.Value));
		}
	}
}
