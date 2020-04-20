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


		public Session CreateSession(Guid userId)
		{
			IEnumerable<TemplateQuestion> questions = TemplateQuestionRepository.GetAll();

			Session session = new Session(userId, questions);

			SessionRepository.Insert(session);

			return session;
		}


		public void JoinTheSession(Guid sessionId, Guid userId)
		{
			Session session = GetSessionById(sessionId);

			session.AddParticipant(userId);
		}


		public void EnableAnswersOfTheCurrentQuestion(Guid sessionId, Guid userId)
		{
			Session session = GetSessionById(sessionId);

			session.EnableAnswersOfTheCurrentQuestion(userId);
		}


		public void AnswerTheCurrentQuestion(Guid userId, Answer answer, Guid sessionId)
		{
			Session session = GetSessionById(sessionId);

			session.AnswerTheCurrentQuestion(userId, answer);
		}


		public Session GetSessionById(Guid sessionId)
		{
			Session session = SessionRepository.GetById(sessionId);

			ValidateIfSessionExists(session);

			return session;
		}

		private void ValidateIfSessionExists(Session session)
		{
			if (session == null)
				throw new NonExistentSessionException();
		}
	}
}
