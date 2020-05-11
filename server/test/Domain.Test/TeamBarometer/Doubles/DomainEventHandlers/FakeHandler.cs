using Domain.TeamBarometer.Events;
using DomainEventManager;
using System;
using System.Collections.Generic;

namespace Domain.Test.TeamBarometer.Doubles.DomainEventHandlers
{
	public class FakeHandler : Handler<MeetingEventBase>
	{
		private static List<Guid> notifiedMeetings = new List<Guid>();

		public static bool MeetingWasNotified(Guid meetingId)
		{
			return notifiedMeetings.Contains(meetingId);
		}

		public override void Handle(MeetingEventBase domainEvent)
		{
			notifiedMeetings.Add(domainEvent.Meeting.Id);
		}
	}
}