using Domain.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Sessions
{
	public class SessionModel
	{
		public SessionModel(Session session)
		{
			Id = session.Id;
			Questions = session.Questions.Select(question => new QuestionModel(question));
		}

		public Guid Id { get; set; }
		public IEnumerable<QuestionModel> Questions { get; private set; }
	}
}
