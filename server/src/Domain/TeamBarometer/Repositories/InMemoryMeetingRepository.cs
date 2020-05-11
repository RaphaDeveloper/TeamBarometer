using Domain.TeamBarometer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.TeamBarometer.Repositories
{
	public class InMemoryMeetingRepository
	{
		private readonly List<Meeting> meetings = new List<Meeting>();

		public Meeting GetById(Guid meetingId)
		{
			return meetings.FirstOrDefault(s => s.Id == meetingId);
		}

		public void Insert(Meeting meeting)
		{
			meetings.Add(meeting);
		}
	}
}
