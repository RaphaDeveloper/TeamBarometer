using Domain.TeamBarometer.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Domain.Test.TeamBarometer.Entities
{
	public class MeetingShould
	{
		Guid facilitatorId = Guid.NewGuid();

		[Test]
		public void HasId()
		{
			Meeting meeting = CreateMeeting();

			Assert.That(meeting.Id, Is.Not.Null);
		}

		[Test]
		public void HasUniqueId()
		{
			Meeting firstMeeting = CreateMeeting();
			Meeting secondMeeting = CreateMeeting();
			Meeting thirdMeeting = CreateMeeting();

			Assert.That(firstMeeting.Id, Is.Not.EqualTo(secondMeeting.Id));
			Assert.That(secondMeeting.Id, Is.Not.EqualTo(thirdMeeting.Id));
		}

		[Test]
		public void HasReadOnlyId()
		{
			Meeting meeting = CreateMeeting();

			bool idIsReadOnly = !meeting.GetType().GetProperty(nameof(meeting.Id)).CanWrite;

			Assert.True(idIsReadOnly, "Meeting id is not read only");
		}

		private Meeting CreateMeeting()
		{
			List<TemplateQuestion> questionsTemplate = new List<TemplateQuestion>
			{
				new TemplateQuestion("Confiança", null),
				new TemplateQuestion("Feedback", null)
			};

			return new Meeting(facilitatorId, questionsTemplate);
		}
	}
}
