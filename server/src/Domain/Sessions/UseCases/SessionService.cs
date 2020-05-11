using Domain.Sessions.Entities;
using Domain.Sessions.Exceptions;
using Domain.Sessions.Repositories;
using System;
using System.Collections.Generic;

namespace Domain.Sessions.UseCases
{
	public class SessionService
	{
		private InMemorySessionRepository SessionRepository { get; }
		private TemplateQuestionRepository TemplateQuestionRepository { get; }

		public SessionService(InMemorySessionRepository sessionRepository, TemplateQuestionRepository templateQuestionRepository)
		{
			SessionRepository = sessionRepository;
			TemplateQuestionRepository = templateQuestionRepository;
		}


		public Meeting CreateSession(Guid userId)
		{
			IEnumerable<TemplateQuestion> questions = TemplateQuestionRepository.GetAll();

			Meeting session = new Meeting(userId, questions);

			SessionRepository.Insert(session);

			return session;
		}


		public void JoinTheSession(Guid sessionId, Guid userId)
		{
			Meeting session = GetSessionById(sessionId);

			session.AddParticipant(userId);
		}


		public void EnableAnswersOfTheCurrentQuestion(Guid sessionId, Guid userId)
		{
			Meeting session = GetSessionById(sessionId);

			session.EnableAnswersOfTheCurrentQuestion(userId);
		}


		public void AnswerTheCurrentQuestion(Guid userId, Answer answer, Guid sessionId)
		{
			Meeting session = GetSessionById(sessionId);

			session.AnswerTheCurrentQuestion(userId, answer);
		}


		public Meeting GetSessionById(Guid sessionId)
		{
			Meeting session = SessionRepository.GetById(sessionId);

			ValidateIfSessionExists(session);

			return session;
		}

		private void ValidateIfSessionExists(Meeting session)
		{
			if (session == null)
				throw new NonExistentSessionException();
		}
	}
}
