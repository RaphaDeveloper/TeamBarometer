using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Sessions
{
	public class Question
	{
		public Question(TemplateQuestion templateQuestion)
		{
			TemplateQuestion = templateQuestion;
		}

		public TemplateQuestion TemplateQuestion { get; }
		public Guid Id => TemplateQuestion.Id;
		public string Description => TemplateQuestion.Description;

		private List<Answer> Answers { get; set; } = new List<Answer>();
		private List<Guid> UsersWhoAnswered { get; set; } = new List<Guid>();
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
			return TemplateQuestion.GetDescriptionOfTheAnswer(answer);
		}

		internal void SetAsCurrent()
		{
			IsTheCurrent = true;
		}

		internal void SetAsNotCurrent()
		{
			IsTheCurrent = false;
		}
	}
}
