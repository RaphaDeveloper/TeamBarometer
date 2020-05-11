using API.Hubs;
using Domain.TeamBarometer.Events;
using DomainEventManager;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace API.DomainEventHandlers
{
	public class RefreshMeeting : Handler<MeetingEventBase>
	{
		public RefreshMeeting(IServiceProvider serviceProvider)
		{
			ServiceProvider = serviceProvider;
		}

		public IServiceProvider ServiceProvider { get; }

		public override void Handle(MeetingEventBase domainEvent)
		{
			ServiceProvider.GetService<MeetingHub>().NotifyMeeting(domainEvent.Meeting.Id);
		}
	}
}
