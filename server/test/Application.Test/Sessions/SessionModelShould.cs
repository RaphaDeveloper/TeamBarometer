﻿using Application.Sessions;
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
			Guid teamMemberId = Guid.NewGuid();
			Session session = new Session(teamMemberId, questionTemplates);

			SessionModel sessionModel = new SessionModel(session, teamMemberId);

			Assert.AreEqual(session.Id, sessionModel.Id);
		}

		[Test]
		public void HasTeamMemberAsFacilitatorWhenTheTeamMemberIsEqualsTheFacilitator()
		{
			Guid teamMemberId = Guid.NewGuid();
			Session session = new Session(teamMemberId, questionTemplates);

			SessionModel sessionModel = new SessionModel(session, teamMemberId);

			Assert.IsTrue(sessionModel.TeamMemberIsTheFacilitator);
		}

		[Test]
		public void HasNotTeamMemberAsFacilitatorWhenTheTeamMemberIsNotEqualsTheFacilitator()
		{
			Guid facilitatorId = Guid.NewGuid();
			Guid teamMemberId = Guid.NewGuid();
			Session session = new Session(facilitatorId, questionTemplates);

			SessionModel sessionModel = new SessionModel(session, teamMemberId);

			Assert.IsFalse(sessionModel.TeamMemberIsTheFacilitator);
		}
	}
}