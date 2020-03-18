using Domain.Sessions;
using Domain.Sessions.Repositories;
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
			Session session = SessionService.CreateSession(userId);

			return new SessionModel(session, userId);
		}

		public SessionModel JoinTheSession(Guid sessionId, Guid userId)
		{
			Session session = SessionService.JoinTheSession(sessionId, userId);

			return new SessionModel(session, userId);
		}

		public void EnableAnswersOfTheCurrentQuestion(Guid sessionId, Guid userId)
		{
			SessionService.EnableAnswersOfTheCurrentQuestion(sessionId, userId);
		}

		public SessionModel GetSession(Guid sessionId, Guid userId)
		{
			Session session = SessionService.GetSessionById(sessionId);

			return new SessionModel(session, userId);
		}
	}
}
