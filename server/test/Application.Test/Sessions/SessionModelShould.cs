using Application.Sessions;
using Domain.Sessions;
using Domain.Sessions.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Application.Test.Sessions
{
	public class SessionModelShould
	{
		private InMemoryTemplateQuestionRepository questionTemplateRepository = new InMemoryTemplateQuestionRepository();
		private IEnumerable<TemplateQuestion> questionTemplates;

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
