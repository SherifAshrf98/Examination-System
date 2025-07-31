using Microsoft.AspNetCore.SignalR;

namespace Examination.Api.SignalR.Hubs
{
	public class NotificationHub : Hub
	{
		public override async Task OnConnectedAsync()
		{
			var user = Context.User;

			if (user?.IsInRole("Admin") == true)
			{
				await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
			}

			await base.OnConnectedAsync();
		}
		public async Task SendTestMessage()
		{
			await Clients.Caller.SendAsync("ReceiveNotification", new
			{
				id = Guid.NewGuid().ToString(),
				message = "Connection successful!",
				timestamp = DateTime.UtcNow,
				type = "success"
			});
		}
	}
}
