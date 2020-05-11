using Domain.Sessions.Entities;
using Domain.Sessions.UseCases;
using System;

namespace Application.Sessions.UseCases
{
	public class SessionAppService
	{
		public SessionAppService(SessionService sessionService)
		{
			SessionService = sessionService;
		}

		private SessionService SessionService { get; }

		public SessionModel CreateSession(Guid userId)
		{
			Meeting session = SessionService.CreateSession(userId);

			return new SessionModel(session, userId);
		}

		public SessionModel JoinTheSession(Guid sessionId, Guid userId)
		{
			SessionService.JoinTheSession(sessionId, userId);

			return GetSession(sessionId, userId);
		}

		public void EnableAnswersOfTheCurrentQuestion(Guid sessionId, Guid userId)
		{
			SessionService.EnableAnswersOfTheCurrentQuestion(sessionId, userId);
		}

		public SessionModel GetSession(Guid sessionId, Guid userId)
		{
			Meeting session = SessionService.GetSessionById(sessionId);

			return new SessionModel(session, userId);
		}

		public void AnswerTheCurrentQuestion(Guid userId, Answer answer, Guid sessionId)
		{
			SessionService.AnswerTheCurrentQuestion(userId, answer, sessionId);
		}
	}
}
