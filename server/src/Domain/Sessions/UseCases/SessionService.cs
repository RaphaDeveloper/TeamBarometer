using Domain.Sessions.Repositories;
using System;
using System.Collections.Generic;

namespace Domain.Sessions.UseCases
{
	public class SessionService
	{
		private InMemorySessionRepository SessionRepository { get; }
		private InMemoryTemplateQuestionRepository QuestionTemplateRepository { get; }

		public SessionService(InMemorySessionRepository sessionRepository, InMemoryTemplateQuestionRepository questionTemplateRepository)
		{
			SessionRepository = sessionRepository;
			QuestionTemplateRepository = questionTemplateRepository;
		}

		public Session CreateSession(Guid userId)
		{
			IEnumerable<TemplateQuestion> questions = QuestionTemplateRepository.GetAll();

			Session session = new Session(userId, questions);

			SessionRepository.Insert(session);

			return session;
		}

		public Session JoinTheSession(Guid sessionId, Guid userId)
		{
			Session session = SessionRepository.GetById(sessionId);

			session.AddParticipant(userId);

			return session;
		}

		public void EnableAnswersOfTheCurrentQuestion(Guid sessionId, Guid userId)
		{
			Session session = SessionRepository.GetById(sessionId);

			session.EnableAnswersOfTheCurrentQuestion(userId);
		}

		public Session GetSessionById(Guid sessionId)
		{
			return SessionRepository.GetById(sessionId);
		}

		public void AnswerTheSessionCurrentQuestion(Guid userId, Answer answer, Guid sessionId)
		{
			Session session = SessionRepository.GetById(sessionId);

			session.AnswerTheCurrentQuestion(userId, answer);
		}
	}
}
