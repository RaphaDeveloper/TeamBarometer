using Domain.Sessions.Entities;

namespace Domain.Sessions.Events
{
	public class WhenAllUsersAnswerTheQuestion : SessionEventBase
	{
		public WhenAllUsersAnswerTheQuestion(Meeting session)
			: base(session)
		{
		}
	}
}
