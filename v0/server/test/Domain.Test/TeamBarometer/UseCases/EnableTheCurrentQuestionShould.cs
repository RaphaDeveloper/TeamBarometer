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
	public class EnableTheCurrentQuestionShould
	{
		private Guid facilitatorId = Guid.NewGuid();

		public EnableTheCurrentQuestionShould()
		{
			Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
			serviceProviderMock.Setup(s => s.GetService(typeof(FakeHandler))).Returns(new FakeHandler());

			DomainEvent.Bind<WhenTheQuestionIsEnabled, FakeHandler>(serviceProviderMock.Object);
		}

		[Test]
		public void EnableAnswersOfTheCurrentQuestionWhenTheUserIsTheFacilitator()
		{
			MeetingService service = CreateService();
			Meeting meeting = service.CreateMeeting(facilitatorId);


			service.EnableAnswersOfTheCurrentQuestion(meeting.Id, facilitatorId);


			Assert.True(meeting.CurrentQuestion.IsUpForAnswer);
		}

		[Test]
		public void DispatchEventWhenTheUserIsTheFacilitator()
		{
			MeetingService service = CreateService();
			Meeting meeting = service.CreateMeeting(facilitatorId);


			service.EnableAnswersOfTheCurrentQuestion(meeting.Id, facilitatorId);


			Assert.True(FakeHandler.MeetingWasNotified(meeting.Id));
		}

		[Test]
		public void NotEnableAnswersOfTheCurrentQuestionWhenTheUserIsNotTheFacilitator()
		{
			Guid userId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting meeting = service.CreateMeeting(facilitatorId);


			service.EnableAnswersOfTheCurrentQuestion(meeting.Id, userId);


			Assert.False(meeting.CurrentQuestion.IsUpForAnswer);
		}

		[Test]
		public void NotDispatchEventWhenTheUserIsNotTheFacilitator()
		{
			Guid userId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting meeting = service.CreateMeeting(facilitatorId);


			service.EnableAnswersOfTheCurrentQuestion(meeting.Id, userId);


			Assert.False(FakeHandler.MeetingWasNotified(meeting.Id));
		}

		private MeetingService CreateService()
		{
			InMemoryMeetingRepository meetingRepository = new InMemoryMeetingRepository();

			FakeTemplateQuestionRepository questionRepository = new FakeTemplateQuestionRepository();

			return new MeetingService(meetingRepository, questionRepository);
		}
	}
}
