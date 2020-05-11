using Domain.TeamBarometer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Sessions
{
	public class SessionModel
	{
		public SessionModel(Meeting session, Guid userId)
		{
			Id = session.Id;
			Questions = session.Questions.Select(question => new QuestionModel(question));
			UserIsTheFacilitator = session.UserIsTheFacilitator(userId);
		}

		public Guid Id { get; set; }
		public IEnumerable<QuestionModel> Questions { get; private set; }
		public bool UserIsTheFacilitator { get; set; }

		public override bool Equals(object obj)
		{
			SessionModel session = obj as SessionModel;

			return this.Id == session?.Id;
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}
