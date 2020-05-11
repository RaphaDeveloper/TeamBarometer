using Domain.TeamBarometer.Entities;

namespace Domain.TeamBarometer.Events
{
	public class WhenTheQuestionIsEnabled : MeetingEventBase
	{
		public WhenTheQuestionIsEnabled(Meeting meeting)
			: base(meeting)
		{
		}
	}
}
