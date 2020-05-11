using Domain.TeamBarometer.Entities;
using Domain.TeamBarometer.Exceptions;
using Domain.TeamBarometer.Repositories;
using Domain.TeamBarometer.UseCases;
using Domain.Test.Sessions.Doubles.Repositories;
using NUnit.Framework;
using System;

namespace Domain.Test.Sessions.UseCases
{
	public class JoinTheSessionShould
	{
		private Guid facilitatorId = Guid.NewGuid();

		[Test]
		public void JoinTheSessionWhenTheUserIsNotInTheSession()
		{
			MeetingService sessionService = CreateService();
			Meeting session = sessionService.CreateMeeting(facilitatorId);
			Guid userId = Guid.NewGuid();

			sessionService.JoinTheMeeting(session.Id, userId);

			Assert.True(session.UserIsParticipating(userId));
		}

		[Test]
		public void NotJoinTheSessionWhenTheUserIsAlreadyInTheSession()
		{
			MeetingService sessionService = CreateService();
			Meeting session = sessionService.CreateMeeting(facilitatorId);
			Guid userId = Guid.NewGuid();

			sessionService.JoinTheMeeting(session.Id, userId);
			sessionService.JoinTheMeeting(session.Id, userId);

			Assert.That(session.NumberOfParticipants, Is.EqualTo(1));
		}

		[Test]
		public void ReturnNullWhenTheSessionDoesNotExists()
		{
			MeetingService sessionService = CreateService();
			Guid sessionId = Guid.NewGuid();
			Guid userId = Guid.NewGuid();

			Assert.Throws<NonExistentMeetingException>(() => sessionService.JoinTheMeeting(sessionId, userId));
		}

		private MeetingService CreateService()
		{
			InMemoryMeetingRepository sessionRepository = new InMemoryMeetingRepository();

			FakeTemplateQuestionRepository questionRepository = new FakeTemplateQuestionRepository();

			return new MeetingService(sessionRepository, questionRepository);
		}
	}
}
