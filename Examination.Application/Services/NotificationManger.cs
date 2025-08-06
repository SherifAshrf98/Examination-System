using Examination.Application.Common;
using Examination.Application.Dtos;
using Examination.Application.Interfaces;
using Examination.Application.Interfaces.Repositories;
using Examination.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Services
{
	public class NotificationManger : INotificationManger
	{
		private readonly INotificationService _notificationService;
		private readonly INotificationRepository _notificationRepository;

		public NotificationManger(INotificationService notificationService, INotificationRepository notificationRepository)
		{
			_notificationService = notificationService;

			_notificationRepository = notificationRepository;
		}
		public async Task NotifyStudentAsync(string studentId, string message)
		{
			await _notificationService.NotifyStudentAsync(studentId, message);

			var notificationMessage = new NotificationMessage
			{
				UserId = studentId,
				Message = message,
				UserRole = null,
				IsRead = false,
				Timestamp = DateTime.UtcNow,
			};
			await _notificationRepository.AddNotificationAsync(notificationMessage);
		}
		public async Task NotifyAdminsAsync(string message)
		{
			await _notificationService.NotifyAdminAsync(message);

			var notificationMessage = new NotificationMessage
			{
				UserId = null,
				Message = message,
				UserRole = "Admin",
				IsRead = false,
				Timestamp = DateTime.UtcNow,
			};

			await _notificationRepository.AddNotificationAsync(notificationMessage);
		}

		public async Task<Result<List<NotificationMessageDto>>> GetNotificationsForStudentAsync(string userId)
		{
			var notifications = await _notificationRepository.GetNotificationsByUserIdAsync(userId);

			if (notifications == null || !notifications.Any())
			{
				return Result<List<NotificationMessageDto>>.Failure("No notifications found for this student.");
			}

			return Result<List<NotificationMessageDto>>.Success(
				notifications.Select(n => new NotificationMessageDto
				{
					Id = n.Id,
					Message = n.Message,
					Timestamp = n.Timestamp,
					IsRead = n.IsRead
				}).ToList());
		}

		public async Task<Result<List<NotificationMessageDto>>> GetNotificationsForRoleAsync(string role)
		{
			var notifications = await _notificationRepository.GetNotificationsByUserRoleAsync("Admin");

			if (notifications == null || !notifications.Any())
			{
				return Result<List<NotificationMessageDto>>.Failure("No notifications found for this student.");
			}

			return Result<List<NotificationMessageDto>>.Success(
				notifications.Select(n => new NotificationMessageDto
				{
					Id = n.Id,
					Message = n.Message,
					Timestamp = n.Timestamp,
					IsRead = n.IsRead

				}).ToList());
		}

		public async Task<Result<bool>> MarkNotificationAsReadAsync(string notificationId)
		{
			var result = await _notificationRepository.MarkAsReadAsync(notificationId);

			if (!result)
			{
				return Result<bool>.Failure("Failed to mark notification as read.");
			}

			return Result<bool>.Success(true);
		}
	}
}
