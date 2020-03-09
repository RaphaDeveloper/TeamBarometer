namespace Domain.Sessions.Events
{
	public class WhenTheQuestionIsEnabled
	{
		public WhenTheQuestionIsEnabled(Session session)
		{
			Session = session;
		}

		public Session Session { get; }
	}
}
