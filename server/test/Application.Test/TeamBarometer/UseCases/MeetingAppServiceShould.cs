using Application.TeamBarometer.Models;
using Application.TeamBarometer.UseCases;
using Domain.TeamBarometer.Entities;
using Domain.TeamBarometer.Repositories;
using Domain.TeamBarometer.UseCases;
using Domain.Test.TeamBarometer.Doubles.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Test.TeamBarometer.UseCases
{
	public class MeetingAppServiceShould
	{
		private Guid facilitatorId = Guid.NewGuid();
		private IEnumerable<TemplateQuestion> questionTemplates;
		private MeetingAppService meetingAppService;

		[SetUp]
		public void Setup()
		{
			InMemoryMeetingRepository meetingRepository = new InMemoryMeetingRepository();
			FakeTemplateQuestionRepository questionTemplateRepository = new FakeTemplateQuestionRepository();
			MeetingService meetingService = new MeetingService(meetingRepository, questionTemplateRepository);

			meetingAppService = new MeetingAppService(meetingService);
			questionTemplates = questionTemplateRepository.GetAll();
		}

		[Test]
		public void CreateMeeting()
		{
			MeetingModel meetingModel = meetingAppService.CreateMeeting(facilitatorId);

			Assert.That(meetingModel.Id, Is.Not.EqualTo(Guid.Empty));
			Assert.IsTrue(meetingModel.Questions.First().IsTheCurrent);
			Assert.IsFalse(meetingModel.Questions.First(q => q.IsTheCurrent).IsUpForAnswer);
			Assert.IsTrue(meetingModel.UserIsTheFacilitator);
			AssertThatTheQuestionsHasTheSameDataOfTheTemplate(meetingModel);
			AssertThatTheQuestionsHasNotAnyAmountOfAnswer(meetingModel);
		}
		private void AssertThatTheQuestionsHasTheSameDataOfTheTemplate(MeetingModel meeting)
		{
			for (int i = 0; i < meeting.Questions.Count(); i++)
			{
				QuestionModel questionModel = meeting.Questions.ElementAt(i);
				TemplateQuestion questionTemplate = questionTemplates.ElementAt(i);

				Assert.That(questionModel.Id, Is.Not.EqualTo(Guid.Empty));
				Assert.AreEqual(questionTemplate.Description, questionModel.Description);
				Assert.AreEqual(questionTemplate.GetAnswerDescription(Answer.Red), questionModel.RedAnswer);
				Assert.AreEqual(questionTemplate.GetAnswerDescription(Answer.Green), questionModel.GreenAnswer);
			}
		}
		private void AssertThatTheQuestionsHasNotAnyAmountOfAnswer(MeetingModel meeting)
		{
			Assert.AreEqual(0, meeting.Questions.Sum(question => question.AmountOfRedAnswers));
			Assert.AreEqual(0, meeting.Questions.Sum(question => question.AmountOfYellowAnswers));
			Assert.AreEqual(0, meeting.Questions.Sum(question => question.AmountOfGreenAnswers));
		}


		[Test]
		public void JoinTheMeeting()
		{
			Guid userId = Guid.NewGuid();

			MeetingModel createdMeeting = meetingAppService.CreateMeeting(facilitatorId);
			MeetingModel joinedMeeting = meetingAppService.JoinTheMeeting(createdMeeting.Id, userId);

			Assert.That(joinedMeeting, Is.EqualTo(createdMeeting));
			Assert.IsFalse(joinedMeeting.UserIsTheFacilitator);
			AssertThatTheQuestionsOfTheJoinedMeetingAreEqualToTheQuestionsOfTheCreatedMeeting(joinedMeeting, createdMeeting);
		}
		private void AssertThatTheQuestionsOfTheJoinedMeetingAreEqualToTheQuestionsOfTheCreatedMeeting(MeetingModel joinedMeeting, MeetingModel createdMeeting)
		{
			Assert.That(joinedMeeting.Questions.Count(), Is.EqualTo(createdMeeting.Questions.Count()));

			for (int i = 0; i < joinedMeeting.Questions.Count(); i++)
			{
				QuestionModel questionOfTheJoinedMeeting = joinedMeeting.Questions.ElementAt(i);
				QuestionModel questionOfTheCreatedMeeting = createdMeeting.Questions.ElementAt(i);

				Assert.That(questionOfTheJoinedMeeting, Is.EqualTo(questionOfTheCreatedMeeting));
			}
		}


		[Test]
		public void TurnTheCurrentQuestionUpForAnswerWhenTheUserWhoGetTheMeetingIsNotTheFacilitator()
		{
			Guid facilitatorId = Guid.NewGuid();
			MeetingModel meetingModel = meetingAppService.CreateMeeting(facilitatorId);
			meetingAppService.EnableAnswersOfTheCurrentQuestion(meetingModel.Id, facilitatorId);

			meetingModel = meetingAppService.GetMeeting(meetingModel.Id, facilitatorId);

			AssertThatTheCurrentQuestionIsUpForAnswer(meetingModel);
		}
		private void AssertThatTheCurrentQuestionIsUpForAnswer(MeetingModel meeting)
		{
			Assert.True(meeting.Questions.First(q => q.IsTheCurrent).IsUpForAnswer);
		}


		[Test]
		public void ContabilizeTheAnswersWhenUserAnswerTheCurrentQuestion()
		{
			Guid greenUser = Guid.NewGuid();
			Guid yellowUser = Guid.NewGuid();
			Guid redUser = Guid.NewGuid();
			Guid facilitatorId = Guid.NewGuid();
			MeetingModel meetingModel = meetingAppService.CreateMeeting(facilitatorId);
			meetingAppService.JoinTheMeeting(meetingModel.Id, greenUser);
			meetingAppService.JoinTheMeeting(meetingModel.Id, yellowUser);
			meetingAppService.JoinTheMeeting(meetingModel.Id, redUser);
			meetingAppService.EnableAnswersOfTheCurrentQuestion(meetingModel.Id, facilitatorId);

			meetingAppService.AnswerTheCurrentQuestion(greenUser, Answer.Green, meetingModel.Id);
			meetingAppService.AnswerTheCurrentQuestion(yellowUser, Answer.Yellow, meetingModel.Id);
			meetingAppService.AnswerTheCurrentQuestion(redUser, Answer.Red, meetingModel.Id);

			AssertThatTheAnswerWasContabilized(meetingModel);
		}
		private void AssertThatTheAnswerWasContabilized(MeetingModel meeting)
		{
			MeetingModel meetingModel = meetingAppService.GetMeeting(meeting.Id, facilitatorId);

			Assert.That(meetingModel.Questions.First().AmountOfGreenAnswers, Is.EqualTo(1));
			Assert.That(meetingModel.Questions.First().AmountOfYellowAnswers, Is.EqualTo(1));
			Assert.That(meetingModel.Questions.First().AmountOfRedAnswers, Is.EqualTo(1));
		}

		[Test]
		public void UpdateTheCurrentQuestionWhenAllUsersAnswerTheQuestion()
		{
			Guid greenUser = Guid.NewGuid();
			Guid yellowUser = Guid.NewGuid();
			Guid redUser = Guid.NewGuid();
			Guid facilitatorId = Guid.NewGuid();
			MeetingModel meetingModel = meetingAppService.CreateMeeting(facilitatorId);
			meetingAppService.JoinTheMeeting(meetingModel.Id, greenUser);
			meetingAppService.JoinTheMeeting(meetingModel.Id, yellowUser);
			meetingAppService.JoinTheMeeting(meetingModel.Id, redUser);
			meetingAppService.EnableAnswersOfTheCurrentQuestion(meetingModel.Id, facilitatorId);

			meetingAppService.AnswerTheCurrentQuestion(greenUser, Answer.Green, meetingModel.Id);
			meetingAppService.AnswerTheCurrentQuestion(yellowUser, Answer.Yellow, meetingModel.Id);
			meetingAppService.AnswerTheCurrentQuestion(redUser, Answer.Red, meetingModel.Id);

			AssertThatTheCurrentQuestionHasChanged(meetingModel);
		}
		private void AssertThatTheCurrentQuestionHasChanged(MeetingModel meeting)
		{
			MeetingModel meetingModel = meetingAppService.GetMeeting(meeting.Id, facilitatorId);

			Assert.False(meetingModel.Questions.ElementAt(0).IsTheCurrent);
			Assert.True(meetingModel.Questions.ElementAt(1).IsTheCurrent);
		}
	}
}
