using Domain.Sessions;
using Domain.Sessions.Repositories;
using Domain.Sessions.UseCases;
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
			SessionService sessionService = CreateService();
			Session session = sessionService.CreateSession(facilitatorId);
			Guid userId = Guid.NewGuid();

			sessionService.JoinTheSession(session.Id, userId);

			Assert.True(session.UserIsParticipating(userId));
		}

		[Test]
		public void NotJoinTheSessionWhenTheUserIsAlreadyInTheSession()
		{
			SessionService sessionService = CreateService();
			Session session = sessionService.CreateSession(facilitatorId);
			Guid userId = Guid.NewGuid();

			sessionService.JoinTheSession(session.Id, userId);
			sessionService.JoinTheSession(session.Id, userId);

			Assert.That(session.NumberOfParticipants, Is.EqualTo(1));
		}

		private SessionService CreateService()
		{
			InMemorySessionRepository sessionRepository = new InMemorySessionRepository();

			InMemoryTemplateQuestionRepository questionRepository = new InMemoryTemplateQuestionRepository();

			return new SessionService(sessionRepository, questionRepository);
		}
	}
}
