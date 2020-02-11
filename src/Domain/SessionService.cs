﻿using System;
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
		}

		public Guid Id { get; } = Guid.NewGuid();
		public IEnumerable<Question> Questions { get; set; }
		public AnsweredQuestions AnsweredQuestions { get; set; }
		private Dictionary<Guid, Answers> AnswersByQuestion { get; set; } = new Dictionary<Guid, Answers>();

		internal void AnswerTheQuestion(Guid teamMemberId, Guid questionId, Answer answer)
		{
			if (!AnswersByQuestion.TryGetValue(questionId, out Answers answers))
			{
				answers = new Answers();

				AnswersByQuestion.Add(questionId, answers);
			}

			answers.CountAnswer(teamMemberId, answer);
		}

		public Answers GetTheAnswersOfTheQuestion(Guid questionId)
		{
			return AnswersByQuestion[questionId];
		}
	}

	public class AnsweredQuestions
	{

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
		private Dictionary<Answer, List<Guid>> CountByAnswer { get; set; } = new Dictionary<Answer, List<Guid>>();

		public int GetAnswerCount(Answer answer)
		{
			CountByAnswer.TryGetValue(answer, out List<Guid> members);

			return members.Count;
		}

		internal void CountAnswer(Guid teamMemberId, Answer answer)
		{
			if (!CountByAnswer.TryGetValue(answer, out List<Guid> members))
			{
				members = new List<Guid>();

				CountByAnswer.Add(answer, members);
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
			yield return CreateQuestion();

			yield return CreateQuestion();
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