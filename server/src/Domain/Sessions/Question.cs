using Domain.Questions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Sessions
{
	public class Question
	{
		public Question(QuestionTemplate questionTemplate)
		{
			QuestionTemplate = questionTemplate;
		}

		public QuestionTemplate QuestionTemplate { get; }
		public Guid Id => QuestionTemplate.Id;
		public string Description => QuestionTemplate.Description;

		private List<Answer> Answers { get; set; } = new List<Answer>();
		private List<Guid> UsersWhoAnswered { get; set; } = new List<Guid>();
		public Question NextQuestion { get; internal set; }
		public bool IsUpForAnswer { get; private set; }
		public bool IsTheCurrent { get; internal set; }

		internal void ContabilizeTheAnswer(Guid userId, Answer answer)
		{
			if (!UserHasAlreadyAnswered(userId))
			{
				Answers.Add(answer);

				UsersWhoAnswered.Add(userId);
			}
		}

		private bool UserHasAlreadyAnswered(Guid userId)
		{
			return UsersWhoAnswered.Contains(userId);
		}

		public bool HasAnyAnswer()
		{
			return Answers.Any();
		}

		public int GetCountOfTheAnswer(Answer answer)
		{
			return Answers.Count(a => a == answer);
		}

		internal bool AllUsersHasAnswered(List<Guid> userIds)
		{
			return Answers.Count == userIds.Count;
		}

		internal void EnableAnswers()
		{
			IsUpForAnswer = true;
		}

		internal void DisableAnswers()
		{
			IsUpForAnswer = false;
		}

		public string GetDescriptionOfTheAnswer(Answer answer)
		{
			return QuestionTemplate.GetDescriptionOfTheAnswer(answer);
		}
	}
}
