using Domain.Sessions.Entities;

namespace Domain.Sessions.Events
{
	public abstract class SessionEventBase
	{
		public SessionEventBase(Meeting session)
		{
			Session = session;
		}

		public Meeting Session { get; }
	}
}
