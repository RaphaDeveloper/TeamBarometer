using API.Hubs;
using Domain.TeamBarometer.Events;
using DomainEventManager;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace API.DomainEventHandlers
{
	public class RefreshMeeting : Handler<MeetingEventBase>
	{
		private IServiceProvider serviceProvider;
		
		public RefreshMeeting(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}


		public override void Handle(MeetingEventBase domainEvent)
		{
			serviceProvider.GetService<MeetingHub>().NotifyMeeting(domainEvent.Meeting.Id);
		}
	}
}
