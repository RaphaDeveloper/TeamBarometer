using Application.Sessions;
using Domain.Questions;
using Domain.Sessions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Application.Test.Sessions
{
	public class SessionModelShould
	{
		private InMemoryQuestionTemplateRepository questionTemplateRepository = new InMemoryQuestionTemplateRepository();
		private IEnumerable<QuestionTemplate> questionTemplates;

		[SetUp]
		public void Setup()
		{
			questionTemplates = questionTemplateRepository.GetAll();
		}

		[Test]
		public void HasTheSameIdAsTheSession()
		{
			Session session = new Session(facilitatorId: Guid.NewGuid(), questionTemplates);

			SessionModel sessionModel = new SessionModel(session);

			Assert.AreEqual(session.Id, sessionModel.Id);
		}
	}
}
