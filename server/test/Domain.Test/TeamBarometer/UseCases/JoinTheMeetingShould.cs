using Domain.TeamBarometer.Entities;
using Domain.TeamBarometer.Exceptions;
using Domain.TeamBarometer.Repositories;
using Domain.TeamBarometer.UseCases;
using Domain.Test.TeamBarometer.Doubles.Repositories;
using NUnit.Framework;
using System;

namespace Domain.Test.TeamBarometer.UseCases
{
	public class JoinTheMeetingShould
	{
		private Guid facilitatorId = Guid.NewGuid();

		[Test]
		public void JoinTheMeetingWhenTheUserIsNotParticipatingTheMeeting()
		{
			MeetingService service = CreateService();
			Meeting meeting = service.CreateMeeting(facilitatorId);
			Guid userId = Guid.NewGuid();

			service.JoinTheMeeting(meeting.Id, userId);

			Assert.True(meeting.UserIsParticipating(userId));
		}

		[Test]
		public void NotJoinTheMeetingWhenTheUserIsAlreadyParticipatingTheMeeting()
		{
			MeetingService service = CreateService();
			Meeting meeting = service.CreateMeeting(facilitatorId);
			Guid userId = Guid.NewGuid();

			service.JoinTheMeeting(meeting.Id, userId);
			service.JoinTheMeeting(meeting.Id, userId);

			Assert.That(meeting.NumberOfParticipants, Is.EqualTo(1));
		}

		[Test]
		public void ThrowsAnExceptionWhenTheMeetingDoesNotExists()
		{
			MeetingService service = CreateService();
			Guid meetingId = Guid.NewGuid();
			Guid userId = Guid.NewGuid();

			Assert.Throws<NonExistentMeetingException>(() => service.JoinTheMeeting(meetingId, userId));
		}

		private MeetingService CreateService()
		{
			InMemoryMeetingRepository meetingRepository = new InMemoryMeetingRepository();

			FakeTemplateQuestionRepository questionRepository = new FakeTemplateQuestionRepository();

			return new MeetingService(meetingRepository, questionRepository);
		}
	}
}
