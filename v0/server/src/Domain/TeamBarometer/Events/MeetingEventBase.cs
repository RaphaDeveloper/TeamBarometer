using Domain.TeamBarometer.Entities;

namespace Domain.TeamBarometer.Events
{
	public abstract class MeetingEventBase
	{
		public MeetingEventBase(Meeting metting)
		{
			Meeting = metting;
		}

		public Meeting Meeting { get; }
	}
}
