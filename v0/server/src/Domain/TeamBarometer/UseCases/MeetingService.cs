using Domain.TeamBarometer.Entities;
using Domain.TeamBarometer.Exceptions;
using Domain.TeamBarometer.Repositories;
using System;
using System.Collections.Generic;

namespace Domain.TeamBarometer.UseCases
{
	public class MeetingService
	{
		private InMemoryMeetingRepository MeetingRepository { get; }
		private TemplateQuestionRepository TemplateQuestionRepository { get; }

		public MeetingService(InMemoryMeetingRepository meetingRepository, TemplateQuestionRepository templateQuestionRepository)
		{
			MeetingRepository = meetingRepository;
			TemplateQuestionRepository = templateQuestionRepository;
		}


		public Meeting CreateMeeting(Guid userId)
		{
			IEnumerable<TemplateQuestion> questions = TemplateQuestionRepository.GetAll();

			Meeting meeting = new Meeting(userId, questions);

			MeetingRepository.Insert(meeting);

			return meeting;
		}


		public void JoinTheMeeting(Guid meetingId, Guid userId)
		{
			Meeting meeting = GetMeetingById(meetingId);

			meeting.AddParticipant(userId);
		}


		public void EnableAnswersOfTheCurrentQuestion(Guid meetingId, Guid userId)
		{
			Meeting meeting = GetMeetingById(meetingId);

			meeting.EnableAnswersOfTheCurrentQuestion(userId);
		}


		public void AnswerTheCurrentQuestion(Guid meetingId, Guid userId, Answer answer, string annotation)
		{
			Meeting meeting = GetMeetingById(meetingId);

			meeting.AnswerTheCurrentQuestion(userId, answer, annotation);
		}


		public Meeting GetMeetingById(Guid meetingId)
		{
			Meeting meeting = MeetingRepository.GetById(meetingId);

			ValidateIfMeetingExists(meeting);

			return meeting;
		}

		private void ValidateIfMeetingExists(Meeting meeting)
		{
			if (meeting == null)
				throw new NonExistentMeetingException();
		}
	}
}
