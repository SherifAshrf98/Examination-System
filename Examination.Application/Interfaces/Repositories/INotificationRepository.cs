using Examination.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Interfaces.Repositories
{
	public interface INotificationRepository
	{
		Task AddNotificationAsync(NotificationMessage notification);
		Task<List<NotificationMessage>> GetNotificationsByUserIdAsync(string userId);
		Task<List<NotificationMessage>> GetNotificationsByUserRoleAsync(string userRole);
		Task<bool> MarkAsReadAsync(string notificationId);
	}
}
