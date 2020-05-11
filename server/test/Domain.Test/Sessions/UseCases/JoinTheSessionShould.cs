using Domain.Sessions.Entities;
using Domain.Sessions.Exceptions;
using Domain.Sessions.Repositories;
using Domain.Sessions.UseCases;
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
			SessionService sessionService = CreateService();
			Meeting session = sessionService.CreateSession(facilitatorId);
			Guid userId = Guid.NewGuid();

			sessionService.JoinTheSession(session.Id, userId);

			Assert.True(session.UserIsParticipating(userId));
		}

		[Test]
		public void NotJoinTheSessionWhenTheUserIsAlreadyInTheSession()
		{
			SessionService sessionService = CreateService();
			Meeting session = sessionService.CreateSession(facilitatorId);
			Guid userId = Guid.NewGuid();

			sessionService.JoinTheSession(session.Id, userId);
			sessionService.JoinTheSession(session.Id, userId);

			Assert.That(session.NumberOfParticipants, Is.EqualTo(1));
		}

		[Test]
		public void ReturnNullWhenTheSessionDoesNotExists()
		{
			SessionService sessionService = CreateService();
			Guid sessionId = Guid.NewGuid();
			Guid userId = Guid.NewGuid();

			Assert.Throws<NonExistentSessionException>(() => sessionService.JoinTheSession(sessionId, userId));
		}

		private SessionService CreateService()
		{
			InMemorySessionRepository sessionRepository = new InMemorySessionRepository();

			FakeTemplateQuestionRepository questionRepository = new FakeTemplateQuestionRepository();

			return new SessionService(sessionRepository, questionRepository);
		}
	}
}
