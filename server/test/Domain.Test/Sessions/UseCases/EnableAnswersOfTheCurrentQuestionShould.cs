using Domain.Sessions.Entities;
using Domain.Sessions.Events;
using Domain.Sessions.Repositories;
using Domain.Sessions.UseCases;
using Domain.Test.Sessions.Doubles.DomainEventHandlers;
using Domain.Test.Sessions.Doubles.Repositories;
using DomainEventManager;
using Moq;
using NUnit.Framework;
using System;

namespace Domain.Test.Sessions.UseCases
{
	public class EnableAnswersOfTheCurrentQuestionShould
	{
		private Guid facilitatorId = Guid.NewGuid();

		public EnableAnswersOfTheCurrentQuestionShould()
		{
			Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
			serviceProviderMock.Setup(s => s.GetService(typeof(FakeHandler))).Returns(new FakeHandler());

			DomainEvent.Bind<WhenTheQuestionIsEnabled, FakeHandler>(serviceProviderMock.Object);
		}

		[Test]
		public void EnableAnswersOfTheCurrentQuestionOfTheSessionWhenTheUserIsTheFacilitator()
		{
			SessionService service = CreateService();
			Meeting session = service.CreateSession(facilitatorId);


			service.EnableAnswersOfTheCurrentQuestion(session.Id, facilitatorId);


			Assert.True(session.CurrentQuestion.IsUpForAnswer);
		}

		[Test]
		public void DispatchEventWhenTheUserIsTheFacilitator()
		{
			SessionService service = CreateService();
			Meeting session = service.CreateSession(facilitatorId);


			service.EnableAnswersOfTheCurrentQuestion(session.Id, facilitatorId);


			Assert.True(FakeHandler.SessionWasNotified(session.Id));
		}

		[Test]
		public void NotEnableAnswersOfTheCurrentQuestionOfTheSessionWhenTheUserIsNotTheFacilitator()
		{
			Guid userId = Guid.NewGuid();
			SessionService service = CreateService();
			Meeting session = service.CreateSession(facilitatorId);


			service.EnableAnswersOfTheCurrentQuestion(session.Id, userId);


			Assert.False(session.CurrentQuestion.IsUpForAnswer);
		}

		[Test]
		public void NotDispatchEventWhenTheUserIsNotTheFacilitator()
		{
			Guid userId = Guid.NewGuid();
			SessionService service = CreateService();
			Meeting session = service.CreateSession(facilitatorId);


			service.EnableAnswersOfTheCurrentQuestion(session.Id, userId);


			Assert.False(FakeHandler.SessionWasNotified(session.Id));
		}

		private SessionService CreateService()
		{
			InMemorySessionRepository sessionRepository = new InMemorySessionRepository();

			FakeTemplateQuestionRepository questionRepository = new FakeTemplateQuestionRepository();

			return new SessionService(sessionRepository, questionRepository);
		}
	}
}
