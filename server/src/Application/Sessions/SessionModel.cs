using Domain.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Sessions
{
	public class SessionModel
	{
		public SessionModel(Session session, Guid teamMemberId)
		{
			Id = session.Id;
			Questions = session.Questions.Select(question => new QuestionModel(question));
			TeamMemberIsTheFacilitator = session.TeamMemberIsTheFacilitator(teamMemberId);
		}

		public Guid Id { get; set; }
		public IEnumerable<QuestionModel> Questions { get; private set; }
		public bool TeamMemberIsTheFacilitator { get; set; }
	}
}
