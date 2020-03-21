using API.Hubs;
using Domain.Sessions.Events;
using DomainEventManager;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace API.DomainEventHandlers
{
	public class RefreshSession : Handler<SessionEventBase>
	{
		public RefreshSession(IServiceProvider serviceProvider)
		{
			ServiceProvider = serviceProvider;
		}

		public IServiceProvider ServiceProvider { get; }
		private SessionHub SessionHub { get; }

		public override void Handle(SessionEventBase domainEvent)
		{
			ServiceProvider.GetService<SessionHub>().NotifySession(domainEvent.Session.Id);
		}
	}
}
