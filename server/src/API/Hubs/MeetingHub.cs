using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace API.Hubs
{
	public class MeetingHub : Hub
	{
		public override async Task OnConnectedAsync()
		{
			string meetingId = Context.GetHttpContext().Request.RouteValues["meetingId"] as string;

			await Groups.AddToGroupAsync(Context.ConnectionId, meetingId);

			await base.OnConnectedAsync();
		}

		public async Task NotifyMeeting(Guid meetingId)
		{
			await Clients.Group(meetingId.ToString()).SendAsync("RefreshMeeting");
		}
	}
}
