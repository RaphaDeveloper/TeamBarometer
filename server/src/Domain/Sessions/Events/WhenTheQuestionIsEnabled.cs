using Domain.Sessions.Entities;

namespace Domain.Sessions.Events
{
	public class WhenTheQuestionIsEnabled : SessionEventBase
	{
		public WhenTheQuestionIsEnabled(Meeting session)
			: base(session)
		{
		}
	}
}
