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
	public class AnswerTheCurrentQuestionShould
	{
		private Guid facilitatorId = Guid.NewGuid();

		public AnswerTheCurrentQuestionShould()
		{
			Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
			serviceProviderMock.Setup(s => s.GetService(typeof(FakeHandler))).Returns(new FakeHandler());

			DomainEvent.Bind<WhenAllUsersAnswerTheQuestion, FakeHandler>(serviceProviderMock.Object);
		}

		[Test]
		public void AnswerTheQuestion()
		{
			Guid userId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting meeting = service.CreateMeeting(facilitatorId);
			Question currentQuestion = meeting.CurrentQuestion;
			service.JoinTheMeeting(meeting.Id, userId);
			service.EnableAnswersOfTheCurrentQuestion(meeting.Id, facilitatorId);


			service.AnswerTheCurrentQuestion(userId, Answer.Red, meeting.Id);


			Assert.That(currentQuestion.GetCountOfTheAnswer(Answer.Red), Is.EqualTo(1));
		}

		[Test]
		public void ChangeTheCurrentQuestionWhenAllUsersAnswerTheQuestion()
		{
			Guid firstUserId = Guid.NewGuid();
			Guid secondUserId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting meeting = service.CreateMeeting(facilitatorId);
			Question priorQuestion = meeting.CurrentQuestion;
			service.JoinTheMeeting(meeting.Id, firstUserId);
			service.JoinTheMeeting(meeting.Id, secondUserId);
			service.EnableAnswersOfTheCurrentQuestion(meeting.Id, facilitatorId);


			service.AnswerTheCurrentQuestion(firstUserId, Answer.Green, meeting.Id);
			service.AnswerTheCurrentQuestion(secondUserId, Answer.Green, meeting.Id);


			Assert.That(priorQuestion, Is.Not.EqualTo(meeting.CurrentQuestion));
			Assert.True(meeting.CurrentQuestion.IsTheCurrent);
			Assert.False(priorQuestion.IsTheCurrent);
		}

		[Test]
		public void NotDefineTheCurrentQuestionWhenAllUsersAnswerTheLastQuestion()
		{
			Guid firstUserId = Guid.NewGuid();
			Guid secondUserId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting meeting = service.CreateMeeting(facilitatorId);
			service.JoinTheMeeting(meeting.Id, firstUserId);
			service.JoinTheMeeting(meeting.Id, secondUserId);

			service.EnableAnswersOfTheCurrentQuestion(meeting.Id, facilitatorId);
			service.AnswerTheCurrentQuestion(firstUserId, Answer.Green, meeting.Id);
			service.AnswerTheCurrentQuestion(secondUserId, Answer.Green, meeting.Id);

			service.EnableAnswersOfTheCurrentQuestion(meeting.Id, facilitatorId);
			service.AnswerTheCurrentQuestion(firstUserId, Answer.Green, meeting.Id);
			service.AnswerTheCurrentQuestion(secondUserId, Answer.Green, meeting.Id);


			Assert.IsNull(meeting.CurrentQuestion);
		}

		[Test]
		public void DispatchEventWhenAllUsersAnswerTheQuestion()
		{
			Guid firstUserId = Guid.NewGuid();
			Guid secondUserId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting meeting = service.CreateMeeting(facilitatorId);
			service.JoinTheMeeting(meeting.Id, firstUserId);
			service.JoinTheMeeting(meeting.Id, secondUserId);
			service.EnableAnswersOfTheCurrentQuestion(meeting.Id, facilitatorId);


			service.AnswerTheCurrentQuestion(firstUserId, Answer.Green, meeting.Id);
			service.AnswerTheCurrentQuestion(secondUserId, Answer.Green, meeting.Id);


			Assert.True(FakeHandler.MeetingWasNotified(meeting.Id));
		}

		[Test]
		public void NotDispatchEventWhenAnyUsersNotAnswerTheQuestion()
		{
			Guid firstUserId = Guid.NewGuid();
			Guid secondUserId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting meeting = service.CreateMeeting(facilitatorId);
			service.JoinTheMeeting(meeting.Id, firstUserId);
			service.JoinTheMeeting(meeting.Id, secondUserId);
			service.EnableAnswersOfTheCurrentQuestion(meeting.Id, facilitatorId);


			service.AnswerTheCurrentQuestion(firstUserId, Answer.Green, meeting.Id);


			Assert.False(FakeHandler.MeetingWasNotified(meeting.Id));
		}

		[Test]
		public void DisableAnswersOfTheQuestionWhenAllUsersAnswerIt()
		{
			Guid firstUserId = Guid.NewGuid();
			Guid secondUserId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting meeting = service.CreateMeeting(facilitatorId);
			Question currentQuestion = meeting.CurrentQuestion;
			service.JoinTheMeeting(meeting.Id, firstUserId);
			service.JoinTheMeeting(meeting.Id, secondUserId);
			service.EnableAnswersOfTheCurrentQuestion(meeting.Id, facilitatorId);


			service.AnswerTheCurrentQuestion(firstUserId, Answer.Green, meeting.Id);
			service.AnswerTheCurrentQuestion(secondUserId, Answer.Green, meeting.Id);


			Assert.False(currentQuestion.IsUpForAnswer);
		}

		[Test]
		public void NotAnswerTheQuestionMoreThanOnceForTheSameUser()
		{
			Guid userId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting meeting = service.CreateMeeting(facilitatorId);
			Question currentQuestion = meeting.CurrentQuestion;
			service.JoinTheMeeting(meeting.Id, userId);
			service.EnableAnswersOfTheCurrentQuestion(meeting.Id, facilitatorId);


			service.AnswerTheCurrentQuestion(userId, Answer.Green, meeting.Id);
			service.AnswerTheCurrentQuestion(userId, Answer.Green, meeting.Id);


			Assert.That(currentQuestion.GetCountOfTheAnswer(Answer.Green), Is.EqualTo(1));
		}

		[Test]
		public void NotAnswerTheQuestionWhenTheUserIsNotInThemeeting()
		{
			Guid userId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting meeting = service.CreateMeeting(facilitatorId);
			Question currentQuestion = meeting.CurrentQuestion;
			service.EnableAnswersOfTheCurrentQuestion(meeting.Id, facilitatorId);


			service.AnswerTheCurrentQuestion(userId, Answer.Red, meeting.Id);


			Assert.False(currentQuestion.HasAnyAnswer());
		}

		[Test]
		public void NotAnswerTheQuestionWhenTheQuestionsIsNotUpForAnswer()
		{
			Guid firstUserId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting meeting = service.CreateMeeting(facilitatorId);
			Question currentQuestion = meeting.CurrentQuestion;
			service.JoinTheMeeting(meeting.Id, firstUserId);


			service.AnswerTheCurrentQuestion(firstUserId, Answer.Green, meeting.Id);


			Assert.False(currentQuestion.HasAnyAnswer());
		}

		private MeetingService CreateService()
		{
			InMemoryMeetingRepository meetingRepository = new InMemoryMeetingRepository();

			FakeTemplateQuestionRepository questionRepository = new FakeTemplateQuestionRepository();

			return new MeetingService(meetingRepository, questionRepository);
		}
	}
}
