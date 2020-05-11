using Domain.Sessions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Sessions.Repositories
{
	public class InMemorySessionRepository
	{
		private readonly List<Meeting> sessions = new List<Meeting>();

		public Meeting GetById(Guid sessionId)
		{
			return sessions.FirstOrDefault(s => s.Id == sessionId);
		}

		public void Insert(Meeting session)
		{
			sessions.Add(session);
		}
	}
}
