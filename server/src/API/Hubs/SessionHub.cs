using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace API.Hubs
{
	public class SessionHub : Hub
	{
		public override async Task OnConnectedAsync()
		{
			string sessionId = Context.GetHttpContext().Request.RouteValues["sessionId"] as string;

			await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);

			await base.OnConnectedAsync();
		}

		public async Task NotifySession(Guid sessionId)
		{
			await Clients.Group(sessionId.ToString()).SendAsync("RefreshSession");
		}
	}
}
