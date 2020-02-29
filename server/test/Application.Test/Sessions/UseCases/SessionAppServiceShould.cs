using Application.Sessions;
using Application.Sessions.UseCases;
using Domain.Questions;
using Domain.Sessions;
using Domain.Sessions.Repositories;
using Domain.Sessions.UseCases;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Test.Sessions.UseCases
{
	public class SessionAppServiceShould
	{
		private Guid facilitatorId = Guid.NewGuid();
		private SessionService sessionService;
		private IEnumerable<QuestionTemplate> questionTemplates;

		[SetUp]
		public void Setup()
		{
			InMemorySessionRepository sessionRepository = new InMemorySessionRepository();
			InMemoryQuestionTemplateRepository questionTemplateRepository = new InMemoryQuestionTemplateRepository();

			sessionService = new SessionService(sessionRepository, questionTemplateRepository);

			questionTemplates = questionTemplateRepository.GetAll();
		}

		[Test]
		public void CreateSession()
		{
			SessionAppService sessionAppService = new SessionAppService(sessionService);

			SessionModel sessionModel = sessionAppService.CreateSession(facilitatorId);

			Assert.That(sessionModel.Id, Is.Not.EqualTo(Guid.Empty));
			Assert.IsTrue(sessionModel.Questions.First().IsTheCurrent);
			Assert.IsTrue(sessionModel.TeamMemberIsTheFacilitator);
			AssertThatTheQuestionsHasTheSameDataOfTheTemplate(sessionModel);
			AssertThatTheQuestionsHasNotAnyAmountOfAnswer(sessionModel);
		}		

		private void AssertThatTheQuestionsHasTheSameDataOfTheTemplate(SessionModel session)
		{
			for (int i = 0; i < session.Questions.Count(); i++)
			{
				QuestionModel questionModel = session.Questions.ElementAt(i);
				QuestionTemplate questionTemplate = questionTemplates.ElementAt(i);

				Assert.That(questionModel.Id, Is.Not.EqualTo(Guid.Empty));
				Assert.AreEqual(questionTemplate.Description, questionModel.Description);
				Assert.AreEqual(questionTemplate.GetDescriptionOfTheAnswer(Answer.Red), questionModel.RedAnswer);
				Assert.AreEqual(questionTemplate.GetDescriptionOfTheAnswer(Answer.Green), questionModel.GreenAnswer);
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
			SessionAppService sessionAppService = new SessionAppService(sessionService);

			SessionModel createdSession = sessionAppService.CreateSession(facilitatorId);
			SessionModel joinedSession = sessionAppService.JoinTheSession(createdSession.Id, userId);

			Assert.That(joinedSession, Is.EqualTo(createdSession));
			Assert.IsFalse(joinedSession.TeamMemberIsTheFacilitator);
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
	}
}
