using Domain.Sessions;
using Domain.Sessions.UseCases;
using System;
using System.Linq;

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

			SessionModel sessionModel = new SessionModel
			{
				Questions = session.Questions.Select(q => new QuestionModel())
			};

			return sessionModel;
		}
	}
}
