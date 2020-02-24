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

			SessionModel session = sessionAppService.CreateSession(facilitatorId);

			Assert.That(session.Id, Is.Not.EqualTo(Guid.Empty));
			Assert.IsTrue(session.Questions.First().IsTheCurrent);
			AssertThatTheQuestionsHasTheSameDataOfTheTemplate(session);
			AssertThatTheQuestionsHasNotHasAnyAmountOfAnswer(session);
		}

		private void AssertThatTheQuestionsHasTheSameDataOfTheTemplate(SessionModel session)
		{
			for (int i = 0; i < session.Questions.Count(); i++)
			{
				QuestionModel questionModel = session.Questions.ElementAt(i);
				QuestionTemplate questionTemplate = questionTemplates.ElementAt(i);

				Assert.AreEqual(questionTemplate.Description, questionModel.Description);
				Assert.AreEqual(questionTemplate.GetDescriptionOfTheAnswer(Answer.Red), questionModel.RedAnswer);
				Assert.AreEqual(questionTemplate.GetDescriptionOfTheAnswer(Answer.Green), questionModel.GreenAnswer);
			}
		}

		private void AssertThatTheQuestionsHasNotHasAnyAmountOfAnswer(SessionModel session)
		{
			Assert.AreEqual(0, session.Questions.Sum(question => question.AmountOfRedAnswers));
			Assert.AreEqual(0, session.Questions.Sum(question => question.AmountOfYellowAnswers));
			Assert.AreEqual(0, session.Questions.Sum(question => question.AmountOfGreenAnswers));
		}
	}
}
