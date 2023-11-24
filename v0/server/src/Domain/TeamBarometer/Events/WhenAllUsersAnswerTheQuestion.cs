using Domain.TeamBarometer.Entities;

namespace Domain.TeamBarometer.Events
{
	public class WhenAllUsersAnswerTheQuestion : MeetingEventBase
	{
		public WhenAllUsersAnswerTheQuestion(Meeting meeting)
			: base(meeting)
		{
		}
	}
}
