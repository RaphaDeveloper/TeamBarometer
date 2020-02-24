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

		public Session CreateSession()
		{
			IEnumerable<QuestionTemplate> questions = QuestionRepository.GetAll();

			Session session = new Session(questions);

			SessionRepository.Insert(session);

			return session;
		}

		public void AddTeamMemberToTheSession(Guid teamMemberId, Guid sessionId)
		{
			Session session = SessionRepository.GetById(sessionId);

			session.AddTeamMember(teamMemberId);
		}

		public void EnableAnswersOfTheCurrentQuestionOfTheSession(Guid sessionId)
		{
			Session session = SessionRepository.GetById(sessionId);

			session.EnableAnswersOfTheCurrentQuestion();
		}

		public void AnswerTheCurrentQuestionOfTheSession(Guid teamMemberId, Answer answer, Guid sessionId)
		{
			Session session = SessionRepository.GetById(sessionId);

			session.AnswerTheCurrentQuestion(teamMemberId, answer);
		}
	}
}
