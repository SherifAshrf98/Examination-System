using Examination.Application.Common;
using Examination.Application.Dtos;
using Examination.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Interfaces
{
	public interface INotificationManger
	{
		Task NotifyStudentAsync(string studentId, string message);
		Task NotifyAdminsAsync(string message);
		Task<Result<List<NotificationMessageDto>>> GetNotificationsForStudentAsync(string userId);
		Task<Result<List<NotificationMessageDto>>> GetNotificationsForRoleAsync(string role);
		Task<Result<bool>> MarkNotificationAsReadAsync(string notificationId);
	}
}
