using Domain.Sessions.Events;
using DomainEventManager;
using System;
using System.Collections.Generic;

namespace Domain.Test.Sessions.Doubles
{
	public class FakeHandler : Handler<WhenTheQuestionIsEnabled>
	{
		private static List<Guid> notifiedSessions = new List<Guid>();

		public static bool SessionWasNotified(Guid sessionId)
		{
			return notifiedSessions.Contains(sessionId);
		}

		public override void Handle(WhenTheQuestionIsEnabled domainEvent)
		{
			notifiedSessions.Add(domainEvent.Session.Id);
		}
	}
}
