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
		IEnumerable<QuestionTemplate> questionTemplates;

		[SetUp]
		public void Setup()
		{
			questionTemplates = GetQuestionTemplates();

			Session session = new Session(facilitatorId, questionTemplates);

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

		[Test]
		public void CreateSessionWithQuestionsAndEachQuestionShouldHasTheSameDataOfTheTemplate()
		{
			SessionAppService sessionAppService = new SessionAppService(sessionServiceMock.Object);

			SessionModel session = sessionAppService.CreateSession(facilitatorId);

			AssertThatTheQuestionsHasTheSameDataOfTheTemplate(session);
		}

		private void AssertThatTheQuestionsHasTheSameDataOfTheTemplate(SessionModel session)
		{
			for (int i = 0; i < session.Questions.Count(); i++)
			{
				QuestionModel questionModel = session.Questions.ElementAt(i);
				QuestionTemplate questionTemplate = questionTemplates.ElementAt(i);

				Assert.AreEqual(questionTemplate.Id, questionModel.Id);
				Assert.AreEqual(questionTemplate.Description, questionModel.Description);
				Assert.AreEqual(questionTemplate.GetDescriptionOfTheAnswer(Answer.Red), questionModel.RedAnswer);
				Assert.AreEqual(questionTemplate.GetDescriptionOfTheAnswer(Answer.Green), questionModel.GreenAnswer);
			}
		}

		private IEnumerable<QuestionTemplate> GetQuestionTemplates()
		{
			Dictionary<Answer, string> descriptionByAnswer = new Dictionary<Answer, string>
			{
				{ Answer.Red, "Não damos feedback" },
				{ Answer.Green, "Damos feedback" }
			};

			return new List<QuestionTemplate>
			{
				new QuestionTemplate("Feedback", descriptionByAnswer)
			}.AsEnumerable();
		}
	}
}
