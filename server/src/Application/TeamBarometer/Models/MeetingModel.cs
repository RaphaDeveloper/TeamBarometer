using Domain.TeamBarometer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.TeamBarometer.Models
{
	public class MeetingModel
	{
		public MeetingModel(Meeting meeting, Guid userId)
		{
			Id = meeting.Id;
			Questions = meeting.Questions.Select(question => new QuestionModel(question));
			UserIsTheFacilitator = meeting.UserIsTheFacilitator(userId);
		}

		public Guid Id { get; set; }
		public IEnumerable<QuestionModel> Questions { get; private set; }
		public bool UserIsTheFacilitator { get; set; }

		public override bool Equals(object obj)
		{
			MeetingModel meeting = obj as MeetingModel;

			return Id == meeting?.Id;
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}
