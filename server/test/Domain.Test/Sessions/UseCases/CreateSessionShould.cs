using Domain.Sessions;
using Domain.Sessions.Repositories;
using Domain.Sessions.UseCases;
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
			SessionService service = CreateService();

			Session session = service.CreateSession(facilitatorId);

			Assert.That(session, Is.Not.Null);
			Assert.IsTrue(session.UserIsTheFacilitator(facilitatorId));
			Assert.That(session.Questions, Is.Not.Null.And.Not.Empty);
		}

		[Test]
		public void PersistTheCreatedSession()
		{
			InMemorySessionRepository sessionRepository = new InMemorySessionRepository();
			SessionService service = CreateService(sessionRepository);

			Session session = service.CreateSession(facilitatorId);

			Assert.That(session, Is.EqualTo(sessionRepository.GetById(session.Id)));
		}

		[Test]
		public void CreateTheSessionWithTheFirstQuestionBeingTheCurrent()
		{
			SessionService service = CreateService();

			Session session = service.CreateSession(facilitatorId);

			Assert.That(session.Questions.First(), Is.EqualTo(session.CurrentQuestion));
			Assert.True(session.CurrentQuestion.IsTheCurrent);
		}

		private SessionService CreateService(InMemorySessionRepository sessionRepository = null)
		{
			sessionRepository ??= new InMemorySessionRepository();

			InMemoryTemplateQuestionRepository questionRepository = new InMemoryTemplateQuestionRepository();

			return new SessionService(sessionRepository, questionRepository);
		}
	}
}
