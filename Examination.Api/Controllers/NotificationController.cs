using Examination.Api.Helpers;
using Examination.Application.Dtos;
using Examination.Application.Interfaces;
using Examination.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Examination.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationController : ControllerBase
	{
		private readonly INotificationManger _notificationManger;

		public NotificationController(INotificationManger notificationManger)
		{
			_notificationManger = notificationManger;
		}
		[HttpGet("Student")]
		public async Task<IActionResult> GetStudentNotifications()
		{
			var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (string.IsNullOrWhiteSpace(studentId))
			{
				return BadRequest(new ApiValidationErrorResponse() { Errors = new List<string> { "Invalid student ID." } });
			}

			var result = await _notificationManger.GetNotificationsForStudentAsync(studentId);

			if (!result.IsSuccess)
			{
				if (result.IsNotFound)
					return NotFound(new ApiResponse(404, result.Errors.FirstOrDefault()));

				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors });
			}

			return Ok(new ApiResponse<List<NotificationMessageDto>>(200, result.Value));
		}

		[HttpGet("Admins")]
		public async Task<IActionResult> GetAdminsNotifications()
		{

			var result = await _notificationManger.GetNotificationsForRoleAsync("Admin");

			if (!result.IsSuccess)
			{
				if (result.IsNotFound)
					return NotFound(new ApiResponse(404, result.Errors.FirstOrDefault()));

				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors });
			}

			return Ok(new ApiResponse<List<NotificationMessageDto>>(200, result.Value));

		}

		[HttpPut("{id}/read")]

		public async Task<IActionResult> MarkAsRead(string id)
		{

			var result = await _notificationManger.MarkNotificationAsReadAsync(id);

			if (!result.IsSuccess)
			{
				if (result.IsNotFound)
					return NotFound(new ApiResponse(404, result.Errors.FirstOrDefault()));

				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors });
			}

			return NoContent();
		}


	}
}
