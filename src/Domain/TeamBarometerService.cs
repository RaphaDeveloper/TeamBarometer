using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Domain
{
	public class TeamBarometerService
	{
		private InMemorySessionRepository SessionRepository { get; }
		private InMemoryQuestionRepository QuestionRepository { get; }

		public TeamBarometerService(InMemorySessionRepository sessionRepository, InMemoryQuestionRepository questionRepository)
		{
			SessionRepository = sessionRepository;
			QuestionRepository = questionRepository;
		}

		public Session CreateSession()
		{
			IEnumerable<Question> questions = QuestionRepository.GetAll();

			Session session = new Session(questions);

			SessionRepository.Insert(session);

			return session;
		}

		public void CheckTheQuestionChoiceOnSession(Guid questionId, QuestionChoice questionChoice, Guid sessionId)
		{
			Session session = SessionRepository.GetById(sessionId);

			session.CheckTheQuestionChoice(questionId, questionChoice);
		}
	}

	public class Session
	{
		public Session(IEnumerable<Question> questions)
		{
			Questions = questions;
		}

		public Guid Id { get; } = Guid.NewGuid();
		public IEnumerable<Question> Questions { get; set; }
		private Dictionary<Guid, ChoicesOfQuestion> ChoicesByQuestion { get; set; } = new Dictionary<Guid, ChoicesOfQuestion>();

		internal void CheckTheQuestionChoice(Guid questionId, QuestionChoice questionChoice)
		{
			if (!ChoicesByQuestion.TryGetValue(questionId, out ChoicesOfQuestion choicesOfQuestion))
			{
				choicesOfQuestion = new ChoicesOfQuestion();

				ChoicesByQuestion.Add(questionId, choicesOfQuestion);
			}

			choicesOfQuestion.CheckChoice(questionChoice);
		}

		public ChoicesOfQuestion GetChoicesOfQuestion(Guid questionId)
		{
			return ChoicesByQuestion[questionId];
		}
	}

	public class Question
	{
		public Guid Id { get; } = Guid.NewGuid();
	}

	public enum QuestionChoice
	{
		Green
	}

	public class ChoicesOfQuestion
	{
		private Dictionary<QuestionChoice, int> CountByChoice { get; set; } = new Dictionary<QuestionChoice, int>();

		public int GetCountByChoice(QuestionChoice questionChoice)
		{
			return CountByChoice[questionChoice];
		}

		public void CheckChoice(QuestionChoice questionChoice)
		{
			CountByChoice.TryGetValue(questionChoice, out int count);

			CountByChoice[questionChoice] = ++count;
		}
	}

	public class InMemoryQuestionRepository
	{
		public IEnumerable<Question> GetAll()
		{
			Question confidence = new Question();

			yield return confidence;
		}
	}

	public class InMemorySessionRepository
	{
		private readonly List<Session> sessions = new List<Session>();

		public Session GetById(Guid sessionId)
		{
			return sessions.FirstOrDefault(s => s.Id == sessionId);
		}

		public void Insert(Session session)
		{
			sessions.Add(session);
		}
	}
}
