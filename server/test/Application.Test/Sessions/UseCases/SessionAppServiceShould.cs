using Application.Sessions;
using Application.Sessions.UseCases;
using Domain.TeamBarometer.Entities;
using Domain.TeamBarometer.Repositories;
using Domain.TeamBarometer.UseCases;
using Domain.Test.TeamBarometer.Doubles.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Test.Sessions.UseCases
{
	public class SessionAppServiceShould
	{
		private Guid facilitatorId = Guid.NewGuid();		
		private IEnumerable<TemplateQuestion> questionTemplates;
		private SessionAppService sessionAppService;

		[SetUp]
		public void Setup()
		{
			InMemoryMeetingRepository sessionRepository = new InMemoryMeetingRepository();
			FakeTemplateQuestionRepository questionTemplateRepository = new FakeTemplateQuestionRepository();
			MeetingService sessionService = new MeetingService(sessionRepository, questionTemplateRepository);
			
			sessionAppService = new SessionAppService(sessionService);
			questionTemplates = questionTemplateRepository.GetAll();
		}

		[Test]
		public void CreateSession()
		{
			SessionModel sessionModel = sessionAppService.CreateSession(facilitatorId);

			Assert.That(sessionModel.Id, Is.Not.EqualTo(Guid.Empty));
			Assert.IsTrue(sessionModel.Questions.First().IsTheCurrent);
			Assert.IsFalse(sessionModel.Questions.First(q => q.IsTheCurrent).IsUpForAnswer);
			Assert.IsTrue(sessionModel.UserIsTheFacilitator);
			AssertThatTheQuestionsHasTheSameDataOfTheTemplate(sessionModel);
			AssertThatTheQuestionsHasNotAnyAmountOfAnswer(sessionModel);
		}		
		private void AssertThatTheQuestionsHasTheSameDataOfTheTemplate(SessionModel session)
		{
			for (int i = 0; i < session.Questions.Count(); i++)
			{
				QuestionModel questionModel = session.Questions.ElementAt(i);
				TemplateQuestion questionTemplate = questionTemplates.ElementAt(i);

				Assert.That(questionModel.Id, Is.Not.EqualTo(Guid.Empty));
				Assert.AreEqual(questionTemplate.Description, questionModel.Description);
				Assert.AreEqual(questionTemplate.GetAnswerDescription(Answer.Red), questionModel.RedAnswer);
				Assert.AreEqual(questionTemplate.GetAnswerDescription(Answer.Green), questionModel.GreenAnswer);
			}
		}
		private void AssertThatTheQuestionsHasNotAnyAmountOfAnswer(SessionModel session)
		{
			Assert.AreEqual(0, session.Questions.Sum(question => question.AmountOfRedAnswers));
			Assert.AreEqual(0, session.Questions.Sum(question => question.AmountOfYellowAnswers));
			Assert.AreEqual(0, session.Questions.Sum(question => question.AmountOfGreenAnswers));
		}


		[Test]
		public void JoinTheSession()
		{
			Guid userId = Guid.NewGuid();
			
			SessionModel createdSession = sessionAppService.CreateSession(facilitatorId);
			SessionModel joinedSession = sessionAppService.JoinTheSession(createdSession.Id, userId);

			Assert.That(joinedSession, Is.EqualTo(createdSession));
			Assert.IsFalse(joinedSession.UserIsTheFacilitator);
			AssertThatTheQuestionsOfTheJoinedSessionIsEqualToTheQuestionsOfTheCreatedSession(joinedSession, createdSession);
		}
		private void AssertThatTheQuestionsOfTheJoinedSessionIsEqualToTheQuestionsOfTheCreatedSession(SessionModel joinedSession, SessionModel createdSession)
		{
			Assert.That(joinedSession.Questions.Count(), Is.EqualTo(createdSession.Questions.Count()));

			for (int i = 0; i < joinedSession.Questions.Count(); i++)
			{
				QuestionModel questionOfTheJoinedSession = joinedSession.Questions.ElementAt(i);
				QuestionModel questionOfTheCreatedSession = createdSession.Questions.ElementAt(i);

				Assert.That(questionOfTheJoinedSession, Is.EqualTo(questionOfTheCreatedSession));
			}
		}


		[Test]
		public void TurnTheCurrentQuestionUpForAnswerWhenTheUserWhoGetTheSessionIsNotTheFacilitator()
		{
			Guid facilitatorId = Guid.NewGuid();
			SessionModel sessionModel = sessionAppService.CreateSession(facilitatorId);
			sessionAppService.EnableAnswersOfTheCurrentQuestion(sessionModel.Id, facilitatorId);

			sessionModel = sessionAppService.GetSession(sessionModel.Id, facilitatorId);

			AssertThatTheCurrentQuestionIsUpForAnswer(sessionModel);
		}
		private void AssertThatTheCurrentQuestionIsUpForAnswer(SessionModel session)
		{
			Assert.True(session.Questions.First(q => q.IsTheCurrent).IsUpForAnswer);
		}


		[Test]
		public void ContabilizeTheAnswersWhenUserAnswerTheCurrentQuestion()
		{
			Guid greenUser = Guid.NewGuid();
			Guid yellowUser = Guid.NewGuid();
			Guid redUser = Guid.NewGuid();
			Guid facilitatorId = Guid.NewGuid();
			SessionModel sessionModel = sessionAppService.CreateSession(facilitatorId);
			sessionAppService.JoinTheSession(sessionModel.Id, greenUser);
			sessionAppService.JoinTheSession(sessionModel.Id, yellowUser);
			sessionAppService.JoinTheSession(sessionModel.Id, redUser);
			sessionAppService.EnableAnswersOfTheCurrentQuestion(sessionModel.Id, facilitatorId);

			sessionAppService.AnswerTheCurrentQuestion(greenUser, Answer.Green, sessionModel.Id);
			sessionAppService.AnswerTheCurrentQuestion(yellowUser, Answer.Yellow, sessionModel.Id);
			sessionAppService.AnswerTheCurrentQuestion(redUser, Answer.Red, sessionModel.Id);

			AssertThatTheAnswerWasContabilized(sessionModel);
		}
		private void AssertThatTheAnswerWasContabilized(SessionModel session)
		{
			SessionModel sessionModel = sessionAppService.GetSession(session.Id, facilitatorId);

			Assert.That(sessionModel.Questions.First().AmountOfGreenAnswers, Is.EqualTo(1));
			Assert.That(sessionModel.Questions.First().AmountOfYellowAnswers, Is.EqualTo(1));
			Assert.That(sessionModel.Questions.First().AmountOfRedAnswers, Is.EqualTo(1));
		}

		[Test]
		public void UpdateTheCurrentQuestionWhenAllUsersAnswerTheQuestion()
		{
			Guid greenUser = Guid.NewGuid();
			Guid yellowUser = Guid.NewGuid();
			Guid redUser = Guid.NewGuid();
			Guid facilitatorId = Guid.NewGuid();
			SessionModel sessionModel = sessionAppService.CreateSession(facilitatorId);
			sessionAppService.JoinTheSession(sessionModel.Id, greenUser);
			sessionAppService.JoinTheSession(sessionModel.Id, yellowUser);
			sessionAppService.JoinTheSession(sessionModel.Id, redUser);
			sessionAppService.EnableAnswersOfTheCurrentQuestion(sessionModel.Id, facilitatorId);

			sessionAppService.AnswerTheCurrentQuestion(greenUser, Answer.Green, sessionModel.Id);
			sessionAppService.AnswerTheCurrentQuestion(yellowUser, Answer.Yellow, sessionModel.Id);
			sessionAppService.AnswerTheCurrentQuestion(redUser, Answer.Red, sessionModel.Id);

			AssertThatTheCurrentQuestionHasChanged(sessionModel);
		}
		private void AssertThatTheCurrentQuestionHasChanged(SessionModel session)
		{
			SessionModel sessionModel = sessionAppService.GetSession(session.Id, facilitatorId);

			Assert.False(sessionModel.Questions.ElementAt(0).IsTheCurrent);
			Assert.True(sessionModel.Questions.ElementAt(1).IsTheCurrent);
		}
	}
}
