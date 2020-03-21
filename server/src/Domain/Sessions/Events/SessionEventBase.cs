namespace Domain.Sessions.Events
{
	public abstract class SessionEventBase
	{
		public SessionEventBase(Session session)
		{
			Session = session;
		}

		public Session Session { get; }
	}
}
