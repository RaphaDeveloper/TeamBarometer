using Domain.TeamBarometer.Entities;
using Domain.TeamBarometer.Events;
using Domain.TeamBarometer.Repositories;
using Domain.TeamBarometer.UseCases;
using Domain.Test.Sessions.Doubles.DomainEventHandlers;
using Domain.Test.Sessions.Doubles.Repositories;
using DomainEventManager;
using Moq;
using NUnit.Framework;
using System;

namespace Domain.Test.Sessions.UseCases
{
	public class AnswerTheSessionCurrentQuestionShould
	{
		private Guid facilitatorId = Guid.NewGuid();

		public AnswerTheSessionCurrentQuestionShould()
		{
			Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
			serviceProviderMock.Setup(s => s.GetService(typeof(FakeHandler))).Returns(new FakeHandler());
			
			DomainEvent.Bind<WhenAllUsersAnswerTheQuestion, FakeHandler>(serviceProviderMock.Object);
		}

		[Test]
		public void AnswerTheSessionQuestion()
		{
			Guid userId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting session = service.CreateMeeting(facilitatorId);
			Question currentQuestion = session.CurrentQuestion;
			service.JoinTheMeeting(session.Id, userId);
			service.EnableAnswersOfTheCurrentQuestion(session.Id, facilitatorId);


			service.AnswerTheCurrentQuestion(userId, Answer.Red, session.Id);


			Assert.That(currentQuestion.GetCountOfTheAnswer(Answer.Red), Is.EqualTo(1));
		}

		[Test]
		public void ChangeTheCurrentQuestionWhenAllUsersAnswerTheQuestion()
		{
			Guid firstUserId = Guid.NewGuid();
			Guid secondUserId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting session = service.CreateMeeting(facilitatorId);
			Question priorQuestion = session.CurrentQuestion;
			service.JoinTheMeeting(session.Id, firstUserId);
			service.JoinTheMeeting(session.Id, secondUserId);
			service.EnableAnswersOfTheCurrentQuestion(session.Id, facilitatorId);


			service.AnswerTheCurrentQuestion(firstUserId, Answer.Green, session.Id);
			service.AnswerTheCurrentQuestion(secondUserId, Answer.Green, session.Id);


			Assert.That(priorQuestion, Is.Not.EqualTo(session.CurrentQuestion));
			Assert.True(session.CurrentQuestion.IsTheCurrent);
			Assert.False(priorQuestion.IsTheCurrent);
		}

		[Test]
		public void NotDefineTheCurrentQuestionWhenAllUsersAnswerTheLastQuestion()
		{
			Guid firstUserId = Guid.NewGuid();
			Guid secondUserId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting session = service.CreateMeeting(facilitatorId);
			service.JoinTheMeeting(session.Id, firstUserId);
			service.JoinTheMeeting(session.Id, secondUserId);

			service.EnableAnswersOfTheCurrentQuestion(session.Id, facilitatorId);
			service.AnswerTheCurrentQuestion(firstUserId, Answer.Green, session.Id);
			service.AnswerTheCurrentQuestion(secondUserId, Answer.Green, session.Id);

			service.EnableAnswersOfTheCurrentQuestion(session.Id, facilitatorId);
			service.AnswerTheCurrentQuestion(firstUserId, Answer.Green, session.Id);
			service.AnswerTheCurrentQuestion(secondUserId, Answer.Green, session.Id);


			Assert.IsNull(session.CurrentQuestion);
		}

		[Test]
		public void DispatchEventWhenAllUsersAnswerTheQuestion()
		{
			Guid firstUserId = Guid.NewGuid();
			Guid secondUserId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting session = service.CreateMeeting(facilitatorId);
			service.JoinTheMeeting(session.Id, firstUserId);
			service.JoinTheMeeting(session.Id, secondUserId);
			service.EnableAnswersOfTheCurrentQuestion(session.Id, facilitatorId);

			
			service.AnswerTheCurrentQuestion(firstUserId, Answer.Green, session.Id);
			service.AnswerTheCurrentQuestion(secondUserId, Answer.Green, session.Id);

			
			Assert.True(FakeHandler.SessionWasNotified(session.Id));
		}

		[Test]
		public void NotDispatchEventWhenAnyUsersNotAnswerTheQuestion()
		{
			Guid firstUserId = Guid.NewGuid();
			Guid secondUserId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting session = service.CreateMeeting(facilitatorId);
			service.JoinTheMeeting(session.Id, firstUserId);
			service.JoinTheMeeting(session.Id, secondUserId);
			service.EnableAnswersOfTheCurrentQuestion(session.Id, facilitatorId);


			service.AnswerTheCurrentQuestion(firstUserId, Answer.Green, session.Id);


			Assert.False(FakeHandler.SessionWasNotified(session.Id));
		}

		[Test]
		public void DisableAnswersOfTheQuestionWhenAllUsersAnswerIt()
		{
			Guid firstUserId = Guid.NewGuid();
			Guid secondUserId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting session = service.CreateMeeting(facilitatorId);
			Question currentQuestion = session.CurrentQuestion;
			service.JoinTheMeeting(session.Id, firstUserId);
			service.JoinTheMeeting(session.Id, secondUserId);
			service.EnableAnswersOfTheCurrentQuestion(session.Id, facilitatorId);


			service.AnswerTheCurrentQuestion(firstUserId, Answer.Green, session.Id);
			service.AnswerTheCurrentQuestion(secondUserId, Answer.Green, session.Id);


			Assert.False(currentQuestion.IsUpForAnswer);
		}

		[Test]
		public void NotAnswerTheSessionQuestionMoreThanOnceForTheSameUser()
		{
			Guid userId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting session = service.CreateMeeting(facilitatorId);
			Question currentQuestion = session.CurrentQuestion;
			service.JoinTheMeeting(session.Id, userId);
			service.EnableAnswersOfTheCurrentQuestion(session.Id, facilitatorId);


			service.AnswerTheCurrentQuestion(userId, Answer.Green, session.Id);
			service.AnswerTheCurrentQuestion(userId, Answer.Green, session.Id);


			Assert.That(currentQuestion.GetCountOfTheAnswer(Answer.Green), Is.EqualTo(1));
		}

		[Test]
		public void NotAnswerTheSessionQuestionWhenTheUserIsNotInTheSession()
		{
			Guid userId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting session = service.CreateMeeting(facilitatorId);
			Question currentQuestion = session.CurrentQuestion;
			service.EnableAnswersOfTheCurrentQuestion(session.Id, facilitatorId);


			service.AnswerTheCurrentQuestion(userId, Answer.Red, session.Id);


			Assert.False(currentQuestion.HasAnyAnswer());
		}

		[Test]
		public void NotAnswerTheSessionQuestionWhenTheQuestionsIsNotUpForAnswer()
		{
			Guid firstUserId = Guid.NewGuid();
			MeetingService service = CreateService();
			Meeting session = service.CreateMeeting(facilitatorId);
			Question currentQuestion = session.CurrentQuestion;
			service.JoinTheMeeting(session.Id, firstUserId);


			service.AnswerTheCurrentQuestion(firstUserId, Answer.Green, session.Id);


			Assert.False(currentQuestion.HasAnyAnswer());
		}

		private MeetingService CreateService()
		{
			InMemoryMeetingRepository sessionRepository = new InMemoryMeetingRepository();

			FakeTemplateQuestionRepository questionRepository = new FakeTemplateQuestionRepository();

			return new MeetingService(sessionRepository, questionRepository);
		}
	}
}
