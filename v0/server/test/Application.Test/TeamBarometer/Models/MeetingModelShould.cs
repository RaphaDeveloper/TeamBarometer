using Application.TeamBarometer.Models;
using Domain.TeamBarometer.Entities;
using Domain.Test.TeamBarometer.Doubles.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Application.Test.TeamBarometer.Models
{
	public class MeetingModelShould
	{
		private FakeTemplateQuestionRepository questionTemplateRepository = new FakeTemplateQuestionRepository();
		private IEnumerable<TemplateQuestion> questionTemplates;

		[SetUp]
		public void Setup()
		{
			questionTemplates = questionTemplateRepository.GetAll();
		}

		[Test]
		public void CreateItselfFromMeeting()
		{
			Guid userId = Guid.NewGuid();
			Meeting meeting = new Meeting(userId, questionTemplates);

			MeetingModel meetingModel = new MeetingModel(meeting, userId);

			Assert.AreEqual(meeting.Id, meetingModel.Id);
		}

		[Test]
		public void HasUserAsFacilitatorWhenTheUserIsEqualsTheFacilitator()
		{
			Guid userId = Guid.NewGuid();
			Meeting meeting = new Meeting(userId, questionTemplates);

			MeetingModel meetingModel = new MeetingModel(meeting, userId);

			Assert.IsTrue(meetingModel.UserIsTheFacilitator);
		}

		[Test]
		public void HasNotUserAsFacilitatorWhenTheUserIsNotEqualsTheFacilitator()
		{
			Guid facilitatorId = Guid.NewGuid();
			Guid userId = Guid.NewGuid();
			Meeting meeting = new Meeting(facilitatorId, questionTemplates);

			MeetingModel meetingModel = new MeetingModel(meeting, userId);

			Assert.IsFalse(meetingModel.UserIsTheFacilitator);
		}
	}
}
