using Domain.TeamBarometer.Entities;
using Domain.TeamBarometer.UseCases;
using System;

namespace Application.Sessions.UseCases
{
	public class SessionAppService
	{
		public SessionAppService(MeetingService sessionService)
		{
			SessionService = sessionService;
		}

		private MeetingService SessionService { get; }

		public SessionModel CreateSession(Guid userId)
		{
			Meeting session = SessionService.CreateMeeting(userId);

			return new SessionModel(session, userId);
		}

		public SessionModel JoinTheSession(Guid sessionId, Guid userId)
		{
			SessionService.JoinTheMeeting(sessionId, userId);

			return GetSession(sessionId, userId);
		}

		public void EnableAnswersOfTheCurrentQuestion(Guid sessionId, Guid userId)
		{
			SessionService.EnableAnswersOfTheCurrentQuestion(sessionId, userId);
		}

		public SessionModel GetSession(Guid sessionId, Guid userId)
		{
			Meeting session = SessionService.GetMeetingById(sessionId);

			return new SessionModel(session, userId);
		}

		public void AnswerTheCurrentQuestion(Guid userId, Answer answer, Guid sessionId)
		{
			SessionService.AnswerTheCurrentQuestion(userId, answer, sessionId);
		}
	}
}
