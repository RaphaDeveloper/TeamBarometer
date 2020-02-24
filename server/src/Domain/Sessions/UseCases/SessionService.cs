using Domain.Questions;
using Domain.Sessions.Repositories;
using System;
using System.Collections.Generic;

namespace Domain.Sessions.UseCases
{
	public class SessionService
	{
		private InMemorySessionRepository SessionRepository { get; }
		private InMemoryQuestionTemplateRepository QuestionRepository { get; }

		public SessionService(InMemorySessionRepository sessionRepository, InMemoryQuestionTemplateRepository questionRepository)
		{
			SessionRepository = sessionRepository;
			QuestionRepository = questionRepository;
		}

		public Session CreateSession(Guid facilitatorId)
		{
			IEnumerable<QuestionTemplate> questions = QuestionRepository.GetAll();

			Session session = new Session(facilitatorId, questions);

			SessionRepository.Insert(session);

			return session;
		}

		public void AddTeamMemberInTheSession(Guid teamMemberId, Guid sessionId)
		{
			Session session = SessionRepository.GetById(sessionId);

			session.AddTeamMember(teamMemberId);
		}

		public void EnableAnswersOfTheSessionCurrentQuestion(Guid sessionId)
		{
			Session session = SessionRepository.GetById(sessionId);

			session.EnableAnswersOfTheCurrentQuestion();
		}

		public void AnswerTheSessionCurrentQuestion(Guid teamMemberId, Answer answer, Guid sessionId)
		{
			Session session = SessionRepository.GetById(sessionId);

			session.AnswerTheCurrentQuestion(teamMemberId, answer);
		}
	}
}
