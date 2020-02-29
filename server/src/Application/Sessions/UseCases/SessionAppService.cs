using Domain.Sessions;
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

		public SessionService SessionService { get; }

		public SessionModel CreateSession(Guid facilitatorId)
		{
			Session session = SessionService.CreateSession(facilitatorId);

			return new SessionModel(session, facilitatorId);
		}

		public SessionModel JoinTheSession(Guid sessionId, Guid userId)
		{
			Session session = SessionService.JoinTheSession(sessionId, userId);

			return new SessionModel(session, userId);
		}
	}
}
