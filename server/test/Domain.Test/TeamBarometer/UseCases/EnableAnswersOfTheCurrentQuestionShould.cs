using Domain.TeamBarometer.Entities;
using Domain.TeamBarometer.Events;
using Domain.TeamBarometer.Repositories;
using Domain.TeamBarometer.UseCases;
using Domain.Test.TeamBarometer.Doubles.DomainEventHandlers;
using Domain.Test.TeamBarometer.Doubles.Repositories;
using DomainEventManager;
using Moq;
using NUnit.Framework;
using System;

namespace Domain.Test.TeamBarometer.UseCases
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
			MeetingService service = CreateService();
			Meeting session = service.CreateMeeting(facilitatorId);


			service.EnableAnswersOfTheCurrentQuestion(session.Id, facilitatorId);


			Assert.True(session.CurrentQuestion.IsUpForAnswer);
		}

		[Test]
		public void DispatchEventWhenTheUserIsTheFacilitator()
		{
			MeetingService service = CreateService();
			Meeting session = service.CreateMeeting(facilitatorId);


			service.EnableAnswersOfTheCurrentQuestion(session.Id, facilitatorId);


			Assert.True(FakeHandler.SessionWasNotified(session.Id));
		}

		[Test]
		public void NotEnableAnswersOfTheCurrentQuestionOfTheSessionWhenTheUserIsNotTheFacilitator()
		{
			Guid userId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting session = service.CreateMeeting(facilitatorId);


			service.EnableAnswersOfTheCurrentQuestion(session.Id, userId);


			Assert.False(session.CurrentQuestion.IsUpForAnswer);
		}

		[Test]
		public void NotDispatchEventWhenTheUserIsNotTheFacilitator()
		{
			Guid userId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting session = service.CreateMeeting(facilitatorId);


			service.EnableAnswersOfTheCurrentQuestion(session.Id, userId);


			Assert.False(FakeHandler.SessionWasNotified(session.Id));
		}

		private MeetingService CreateService()
		{
			InMemoryMeetingRepository sessionRepository = new InMemoryMeetingRepository();

			FakeTemplateQuestionRepository questionRepository = new FakeTemplateQuestionRepository();

			return new MeetingService(sessionRepository, questionRepository);
		}
	}
}
