using Domain.Questions;
using Domain.Sessions.Repositories;
using System;
using System.Collections.Generic;

namespace Domain.Sessions.UseCases
{
	public class SessionService
	{
		private InMemorySessionRepository SessionRepository { get; }
		private InMemoryQuestionTemplateRepository QuestionTemplateRepository { get; }

		public SessionService(InMemorySessionRepository sessionRepository, InMemoryQuestionTemplateRepository questionTemplateRepository)
		{
			SessionRepository = sessionRepository;
			QuestionTemplateRepository = questionTemplateRepository;
		}

		public Session CreateSession(Guid userId)
		{
			IEnumerable<QuestionTemplate> questions = QuestionTemplateRepository.GetAll();

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

		public void EnableAnswersOfTheCurrentQuestion(Guid sessionId)
		{
			Session session = SessionRepository.GetById(sessionId);

			session.EnableAnswersOfTheCurrentQuestion();
		}

		public void AnswerTheSessionCurrentQuestion(Guid userId, Answer answer, Guid sessionId)
		{
			Session session = SessionRepository.GetById(sessionId);

			session.AnswerTheCurrentQuestion(userId, answer);
		}
	}
}
