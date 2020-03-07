using DomainEventManager;

namespace Domain.Sessions.Events
{
	public class WhenTheQuestionIsEnabled : IEvent
	{
		public WhenTheQuestionIsEnabled(Session session)
		{
			Session = session;
		}

		public Session Session { get; }
	}
}
