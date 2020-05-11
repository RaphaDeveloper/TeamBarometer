using API.Hubs;
using Domain.TeamBarometer.Events;
using DomainEventManager;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace API.DomainEventHandlers
{
	public class RefreshSession : Handler<MeetingEventBase>
	{
		public RefreshSession(IServiceProvider serviceProvider)
		{
			ServiceProvider = serviceProvider;
		}

		public IServiceProvider ServiceProvider { get; }

		public override void Handle(MeetingEventBase domainEvent)
		{
			ServiceProvider.GetService<SessionHub>().NotifySession(domainEvent.Meeting.Id);
		}
	}
}
