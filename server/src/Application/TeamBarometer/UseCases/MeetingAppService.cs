using Application.TeamBarometer.Models;
using Domain.TeamBarometer.Entities;
using Domain.TeamBarometer.UseCases;
using System;

namespace Application.TeamBarometer.UseCases
{
	public class MeetingAppService
	{
		public MeetingAppService(MeetingService meetingService)
		{
			MeetingService = meetingService;
		}

		private MeetingService MeetingService { get; }

		public MeetingModel CreateMeeting(Guid userId)
		{
			Meeting meeting = MeetingService.CreateMeeting(userId);

			return new MeetingModel(meeting, userId);
		}

		public MeetingModel JoinTheMeeting(Guid meetingId, Guid userId)
		{
			MeetingService.JoinTheMeeting(meetingId, userId);

			return GetMeeting(meetingId, userId);
		}

		public void EnableAnswersOfTheCurrentQuestion(Guid meetingId, Guid userId)
		{
			MeetingService.EnableAnswersOfTheCurrentQuestion(meetingId, userId);
		}

		public MeetingModel GetMeeting(Guid meetingId, Guid userId)
		{
			Meeting meeting = MeetingService.GetMeetingById(meetingId);

			return new MeetingModel(meeting, userId);
		}

		public void AnswerTheCurrentQuestion(Guid meetingId, Guid userId, Answer answer, string annotation)
		{
			MeetingService.AnswerTheCurrentQuestion(meetingId, userId, answer, annotation);
		}
	}
}
