using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Domain
{
	public class SessionService
	{
		private InMemorySessionRepository SessionRepository { get; }
		private InMemoryQuestionRepository QuestionRepository { get; }

		public SessionService(InMemorySessionRepository sessionRepository, InMemoryQuestionRepository questionRepository)
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

		public void AnswerTheSessionQuestion(Guid teamMemberId, Guid questionId, Answer answer, Guid sessionId)
		{
			Session session = SessionRepository.GetById(sessionId);

			session.AnswerTheQuestion(teamMemberId, questionId, answer);
		}
	}

	public class Session
	{
		public Session(IEnumerable<Question> questions)
		{
			Questions = questions;

			InitializeAnswersForEachQuestion(questions);
		}

		public Guid Id { get; } = Guid.NewGuid();
		public IEnumerable<Question> Questions { get; set; }
		private Dictionary<Guid, Answers> AnswersByQuestion { get; set; } = new Dictionary<Guid, Answers>();

		internal void AnswerTheQuestion(Guid teamMemberId, Guid questionId, Answer answer)
		{
			Answers answers = GetTheAnswersOfTheQuestion(questionId);

			answers.ContabilizeTheAnswer(teamMemberId, answer);
		}

		public Answers GetTheAnswersOfTheQuestion(Guid questionId)
		{
			return AnswersByQuestion[questionId];
		}

		private void InitializeAnswersForEachQuestion(IEnumerable<Question> questions)
		{
			foreach (Question question in questions)
			{
				AnswersByQuestion.Add(question.Id, new Answers());
			}
		}
	}

	public class Question
	{
		public Guid Id { get; } = Guid.NewGuid();
	}

	public enum Answer
	{
		Green,
		Red,
		Yellow
	}

	public class Answers
	{
		private Dictionary<Answer, List<Guid>> TeamMembersWhoAlreadyVoted { get; set; } = new Dictionary<Answer, List<Guid>>();

		public int GetAnswerCount(Answer answer)
		{
			TeamMembersWhoAlreadyVoted.TryGetValue(answer, out List<Guid> members);

			return members.Count;
		}

		internal void ContabilizeTheAnswer(Guid teamMemberId, Answer answer)
		{
			if (!TeamMembersWhoAlreadyVoted.TryGetValue(answer, out List<Guid> members))
			{
				members = new List<Guid>();

				TeamMembersWhoAlreadyVoted.Add(answer, members);
			}

			if (!members.Contains(teamMemberId))
			{
				members.Add(teamMemberId);
			}
		}
	}

	public class InMemoryQuestionRepository
	{
		public IEnumerable<Question> GetAll()
		{
			return new List<Question>
			{
				CreateQuestion(),
				CreateQuestion()
			}.AsEnumerable();
		}

		private Question CreateQuestion()
		{
			return new Question();
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
