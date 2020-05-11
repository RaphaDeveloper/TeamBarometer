using Domain.TeamBarometer.Events;
using DomainEventManager;
using System;
using System.Collections.Generic;

namespace Domain.Test.Sessions.Doubles.DomainEventHandlers
{
	public class FakeHandler : Handler<MeetingEventBase>
	{
		private static List<Guid> notifiedSessions = new List<Guid>();

		public static bool SessionWasNotified(Guid sessionId)
		{
			return notifiedSessions.Contains(sessionId);
		}

		public override void Handle(MeetingEventBase domainEvent)
		{
			notifiedSessions.Add(domainEvent.Meeting.Id);
		}
	}
}