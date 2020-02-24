using Application.Sessions;
using Application.Sessions.UseCases;
using Domain.Questions;
using Domain.Sessions;
using Domain.Sessions.UseCases;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Test
{
	public class SessionAppServiceShould
	{
		private Guid facilitatorId = Guid.NewGuid();
		private Mock<ISessionService> sessionServiceMock = new Mock<ISessionService>();

		[SetUp]
		public void Setup()
		{
			Session session = new Session(facilitatorId, GetQuestionTemplates());

			sessionServiceMock.Setup(s => s.CreateSession(facilitatorId)).Returns(session);
		}

		[Test]
		public void CreateSession() 
		{
			SessionAppService sessionAppService = new SessionAppService(sessionServiceMock.Object);

			SessionModel session = sessionAppService.CreateSession(facilitatorId);

			Assert.NotNull(session);
		}

		[Test]
		public void CreateSessionWithQuestions()
		{
			SessionAppService sessionAppService = new SessionAppService(sessionServiceMock.Object);

			SessionModel session = sessionAppService.CreateSession(facilitatorId);

			Assert.AreEqual(GetQuestionTemplates().Count(), session.Questions.Count());
		}

		private IEnumerable<QuestionTemplate> GetQuestionTemplates()
		{
			return new List<QuestionTemplate>
			{
				new QuestionTemplate(),
				new QuestionTemplate()
			}.AsEnumerable();
		}
	}
}
