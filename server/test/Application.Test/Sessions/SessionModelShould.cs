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
		public void CreateItselfFromSession()
		{
			Guid userId = Guid.NewGuid();
			Session session = new Session(userId, questionTemplates);

			SessionModel sessionModel = new SessionModel(session, userId);

			Assert.AreEqual(session.Id, sessionModel.Id);
		}

		[Test]
		public void HasUserAsFacilitatorWhenTheUserIsEqualsTheFacilitator()
		{
			Guid userId = Guid.NewGuid();
			Session session = new Session(userId, questionTemplates);

			SessionModel sessionModel = new SessionModel(session, userId);

			Assert.IsTrue(sessionModel.UserIsTheFacilitator);
		}

		[Test]
		public void HasNotUserAsFacilitatorWhenTheUserIsNotEqualsTheFacilitator()
		{
			Guid facilitatorId = Guid.NewGuid();
			Guid userId = Guid.NewGuid();
			Session session = new Session(facilitatorId, questionTemplates);

			SessionModel sessionModel = new SessionModel(session, userId);

			Assert.IsFalse(sessionModel.UserIsTheFacilitator);
		}
	}
}
