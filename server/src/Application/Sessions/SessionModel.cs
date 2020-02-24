using Domain.Sessions;
using System.Collections.Generic;
using System.Linq;

namespace Application.Sessions
{
	public class SessionModel
	{
		public SessionModel(Session session)
		{
			Questions = session.Questions.Select(question => new QuestionModel(question));
		}

		public IEnumerable<QuestionModel> Questions { get; private set; }
	}
}
