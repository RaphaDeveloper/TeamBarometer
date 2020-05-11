using Domain.TeamBarometer.Entities;
using Domain.TeamBarometer.Repositories;
using Domain.TeamBarometer.UseCases;
using Domain.Test.Sessions.Doubles.Repositories;
using NUnit.Framework;
using System;
using System.Linq;

namespace Domain.Test.Sessions.UseCases
{
	public class CreateSessionShould
	{
		private Guid facilitatorId = Guid.NewGuid();

		[Test]
		public void CreateSessionWithFacilitatorAndQuestions()
		{
			MeetingService service = CreateService();

			Meeting session = service.CreateMeeting(facilitatorId);

			Assert.That(session, Is.Not.Null);
			Assert.IsTrue(session.UserIsTheFacilitator(facilitatorId));
			Assert.That(session.Questions, Is.Not.Null.And.Not.Empty);
		}		

		[Test]
		public void PersistTheCreatedSession()
		{
			InMemoryMeetingRepository sessionRepository = new InMemoryMeetingRepository();
			MeetingService service = CreateService(sessionRepository);

			Meeting session = service.CreateMeeting(facilitatorId);

			Assert.That(session, Is.EqualTo(sessionRepository.GetById(session.Id)));
		}

		[Test]
		public void CreateTheSessionWithTheFirstQuestionBeingTheCurrent()
		{
			MeetingService service = CreateService();

			Meeting session = service.CreateMeeting(facilitatorId);

			Assert.That(session.Questions.First(), Is.EqualTo(session.CurrentQuestion));
			Assert.True(session.CurrentQuestion.IsTheCurrent);
		}

		private MeetingService CreateService(InMemoryMeetingRepository sessionRepository = null)
		{
			sessionRepository ??= new InMemoryMeetingRepository();

			FakeTemplateQuestionRepository questionRepository = new FakeTemplateQuestionRepository();

			return new MeetingService(sessionRepository, questionRepository);
		}
	}
}
