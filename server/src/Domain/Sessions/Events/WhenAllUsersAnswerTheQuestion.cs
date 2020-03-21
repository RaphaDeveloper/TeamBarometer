namespace Domain.Sessions.Events
{
	public class WhenAllUsersAnswerTheQuestion : SessionEventBase
	{
		public WhenAllUsersAnswerTheQuestion(Session session)
			: base(session)
		{
		}
	}
}
