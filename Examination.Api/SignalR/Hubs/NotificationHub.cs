using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Examination.Api.SignalR.Hubs
{
	[Authorize]
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
	}
}
