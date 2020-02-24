using Application.Sessions;
using Application.Sessions.UseCases;
using Domain.Questions;
using Domain.Sessions.Repositories;
using Domain.Sessions.UseCases;
using NUnit.Framework;
using System;
using System.Linq;

namespace Application.Test
{
	public class SessionAppServiceShould
	{
		[Test]
		public void CreateSession() 
		{
			Guid facilitatorId = Guid.NewGuid();
			InMemorySessionRepository sessionRepository = new InMemorySessionRepository();
			InMemoryQuestionTemplateRepository questionRepository = new InMemoryQuestionTemplateRepository();
			SessionService sessionService = new SessionService(sessionRepository, questionRepository);
			SessionAppService sessionAppService = new SessionAppService(sessionService);

			SessionModel session = sessionAppService.CreateSession(facilitatorId);

			Assert.NotNull(session);
		}

		[Test]
		public void CreateSessionWithQuestions()
		{
			Guid facilitatorId = Guid.NewGuid();
			InMemorySessionRepository sessionRepository = new InMemorySessionRepository();
			InMemoryQuestionTemplateRepository questionRepository = new InMemoryQuestionTemplateRepository();
			SessionService sessionService = new SessionService(sessionRepository, questionRepository);
			SessionAppService sessionAppService = new SessionAppService(sessionService);

			SessionModel session = sessionAppService.CreateSession(facilitatorId);

			Assert.AreEqual(questionRepository.GetAll().Count(), session.Questions.Count());
		}
	}
}
