using Domain.TeamBarometer.Entities;
using Domain.TeamBarometer.Repositories;
using Domain.TeamBarometer.UseCases;
using Domain.Test.TeamBarometer.Doubles.Repositories;
using NUnit.Framework;
using System;
using System.Linq;

namespace Domain.Test.TeamBarometer.UseCases
{
	public class CreateMeetingShould
	{
		private Guid facilitatorId = Guid.NewGuid();

		[Test]
		public void CreateMeetingWithFacilitatorAndQuestions()
		{
			MeetingService service = CreateService();

			Meeting meeting = service.CreateMeeting(facilitatorId);

			Assert.That(meeting, Is.Not.Null);
			Assert.IsTrue(meeting.UserIsTheFacilitator(facilitatorId));
			Assert.That(meeting.Questions, Is.Not.Null.And.Not.Empty);
		}

		[Test]
		public void PersistTheCreatedMeeting()
		{
			MeetingService service = CreateService();

			Meeting meeting = service.CreateMeeting(facilitatorId);

			Assert.That(meeting, Is.EqualTo(service.GetMeetingById(meeting.Id)));
		}

		[Test]
		public void CreateMeetingWithTheFirstQuestionBeingTheCurrent()
		{
			MeetingService service = CreateService();

			Meeting meeting = service.CreateMeeting(facilitatorId);

			Assert.That(meeting.Questions.First(), Is.EqualTo(meeting.CurrentQuestion));
			Assert.True(meeting.CurrentQuestion.IsTheCurrent);
		}

		private MeetingService CreateService()
		{
			InMemoryMeetingRepository meetingRepository = new InMemoryMeetingRepository();

			FakeTemplateQuestionRepository questionRepository = new FakeTemplateQuestionRepository();

			return new MeetingService(meetingRepository, questionRepository);
		}
	}
}
