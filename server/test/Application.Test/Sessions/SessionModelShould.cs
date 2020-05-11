using Application.Sessions;
using Domain.Sessions.Entities;
using Domain.Test.Sessions.Doubles.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Application.Test.Sessions
{
	public class SessionModelShould
	{
		private FakeTemplateQuestionRepository questionTemplateRepository = new FakeTemplateQuestionRepository();
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
			Meeting session = new Meeting(userId, questionTemplates);

			SessionModel sessionModel = new SessionModel(session, userId);

			Assert.AreEqual(session.Id, sessionModel.Id);
		}

		[Test]
		public void HasUserAsFacilitatorWhenTheUserIsEqualsTheFacilitator()
		{
			Guid userId = Guid.NewGuid();
			Meeting session = new Meeting(userId, questionTemplates);

			SessionModel sessionModel = new SessionModel(session, userId);

			Assert.IsTrue(sessionModel.UserIsTheFacilitator);
		}

		[Test]
		public void HasNotUserAsFacilitatorWhenTheUserIsNotEqualsTheFacilitator()
		{
			Guid facilitatorId = Guid.NewGuid();
			Guid userId = Guid.NewGuid();
			Meeting session = new Meeting(facilitatorId, questionTemplates);

			SessionModel sessionModel = new SessionModel(session, userId);

			Assert.IsFalse(sessionModel.UserIsTheFacilitator);
		}
	}
}
