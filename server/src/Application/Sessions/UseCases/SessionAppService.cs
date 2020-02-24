using Domain.Sessions;
using Domain.Sessions.UseCases;
using System;
using System.Linq;

namespace Application.Sessions.UseCases
{
	public class SessionAppService
	{
		public SessionAppService(ISessionService sessionService)
		{
			SessionService = sessionService;
		}

		public ISessionService SessionService { get; }

		public SessionModel CreateSession(Guid facilitatorId)
		{
			Session session = SessionService.CreateSession(facilitatorId);

			SessionModel sessionModel = new SessionModel
			{
				Questions = session.Questions.Select(question => new QuestionModel(question))
			};

			return sessionModel;
		}
	}
}
