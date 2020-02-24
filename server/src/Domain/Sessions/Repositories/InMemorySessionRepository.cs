using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Sessions.Repositories
{
	public class InMemorySessionRepository
	{
		private readonly List<Session> sessions = new List<Session>();

		public Session GetById(Guid sessionId)
		{
			return sessions.FirstOrDefault(s => s.Id == sessionId);
		}

		public void Insert(Session session)
		{
			sessions.Add(session);
		}
	}
}
