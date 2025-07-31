using Examination.Api.SignalR.Hubs;
using Examination.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Examination.Api.SignalR.Services
{
	public class NotificationService : INotificationService
	{
		private readonly IHubContext<NotificationHub> _hubContext;

		public NotificationService(IHubContext<NotificationHub> hubContext)
		{
			_hubContext = hubContext;
		}

		public async Task NotifyStudentAsync(string studentId, string message)
		{
			await _hubContext.Clients.User(studentId).SendAsync("ReceiveMessage", message);
		}
		public async Task NotifyAdminAsync(string message)
		{
			await _hubContext.Clients.Group("Admins").SendAsync("ReceiveAdminMessage", message);
		}
	}
}
