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

		public void AnswerTheCurrentQuestionOfTheSession(Guid teamMemberId, Answer answer, Guid sessionId)
		{
			Session session = SessionRepository.GetById(sessionId);

			session.AnswerTheCurrentQuestion(teamMemberId, answer);
		}

		public void AddTeamMemberToTheSession(Guid teamMemberId, Guid sessionId)
		{
			Session session = SessionRepository.GetById(sessionId);

			session.AddTeamMember(teamMemberId);
		}

		public void StartVotingOfTheCurrentQuestionOfTheSession(Guid sessionId)
		{
			Session session = SessionRepository.GetById(sessionId);

			session.StartVotingOfTheCurrentQuestion();
		}
	}

	public class Session
	{
		public Session(IEnumerable<Question> questions)
		{
			ConstructQuestionsOfThisSession(questions);
		}

		public Guid Id { get; } = Guid.NewGuid();
		public Dictionary<Guid, QuestionOfTheSession> QuestionsById { get; set; } = new Dictionary<Guid, QuestionOfTheSession>();
		public QuestionOfTheSession CurrentQuestion { get; private set; }
		private List<Guid> TeamMembers { get; set; } = new List<Guid>();

		internal void AnswerTheCurrentQuestion(Guid teamMemberId, Answer answer)
		{
			if (TeamMemberIsParticipating(teamMemberId) && CurrentQuestion.IsUpForVote)
			{
				CurrentQuestion.ContabilizeTheAnswer(teamMemberId, answer);

				if (CurrentQuestion.AllTeamMembersVoted(TeamMembers))
				{
					CurrentQuestion.StopVoting();

					ChangeTheCurrentQuestion();
				}
			}
		}

		private void ChangeTheCurrentQuestion()
		{
			CurrentQuestion = CurrentQuestion.NextQuestion;
		}

		internal void AddTeamMember(Guid teamMemberId)
		{
			if (TeamMemberIsParticipating(teamMemberId))
			{
				throw new Exception("Team member is already participating of this session.");
			}

			TeamMembers.Add(teamMemberId);
		}

		internal void StartVotingOfTheCurrentQuestion()
		{
			CurrentQuestion.StartVoting();
		}

		public bool TeamMemberIsParticipating(Guid teamMemberId)
		{
			return TeamMembers.Contains(teamMemberId);
		}

		private void ConstructQuestionsOfThisSession(IEnumerable<Question> questions)
		{
			IndexTheQuestionsById(questions);

			DefineTheCurrentQuestion();

			LinkTheQuestions();
		}

		private void IndexTheQuestionsById(IEnumerable<Question> questions)
		{
			foreach (Question question in questions)
			{
				QuestionsById.Add(question.Id, new QuestionOfTheSession(question));
			}
		}

		private void DefineTheCurrentQuestion()
		{
			CurrentQuestion = QuestionsById.Values.First();
		}

		private void LinkTheQuestions()
		{
			QuestionOfTheSession priorQuestion = null;

			foreach (QuestionOfTheSession question in QuestionsById.Values)
			{
				if (priorQuestion != null)
				{
					priorQuestion.NextQuestion = question;
				}

				priorQuestion = question;
			}
		}
	}

	public class Question
	{
		public Guid Id { get; } = Guid.NewGuid();
	}

	public class QuestionOfTheSession
	{
		private readonly Question question;

		public QuestionOfTheSession(Question question)
		{
			this.question = question;
		}

		public Guid Id => question.Id;
		private List<Answer> Answers { get; set; } = new List<Answer>();
		private List<Guid> IdOfTheTeamMembersWhoVoted { get; set; } = new List<Guid>();
		public QuestionOfTheSession NextQuestion { get; internal set; }
		public bool IsUpForVote { get; private set; }

		internal void ContabilizeTheAnswer(Guid teamMemberId, Answer answer)
		{
			if (!TeamMemberHasAlreadyVoted(teamMemberId))
			{
				Answers.Add(answer);

				IdOfTheTeamMembersWhoVoted.Add(teamMemberId);
			}
		}

		private bool TeamMemberHasAlreadyVoted(Guid teamMemberId)
		{
			return IdOfTheTeamMembersWhoVoted.Contains(teamMemberId);
		}

		public bool HasAnyAnswer()
		{
			return Answers.Any();
		}

		public int GetCountOfTheAnswer(Answer answer)
		{
			return Answers.Count(a => a == answer);
		}

		internal bool AllTeamMembersVoted(List<Guid> teamMembersId)
		{
			return Answers.Count == teamMembersId.Count;
		}

		internal void StartVoting()
		{
			IsUpForVote = true;
		}

		internal void StopVoting()
		{
			IsUpForVote = false;
		}
	}

	public enum Answer
	{
		Green,
		Red,
		Yellow
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