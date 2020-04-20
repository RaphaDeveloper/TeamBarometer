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


		private TemplateQuestion TemplateQuestion { get; }
		private List<Answer> Answers { get; } = new List<Answer>();
		private List<Guid> UsersWhoAnswered { get; } = new List<Guid>();
		
		public Guid Id => TemplateQuestion.Id;
		public string Description => TemplateQuestion.Description;
		public bool IsUpForAnswer { get; private set; }
		public bool IsTheCurrent { get; private set; }


		public bool HasAnyAnswer()
		{
			return Answers.Any();
		}


		public int GetCountOfTheAnswer(Answer answer)
		{
			return Answers.Count(a => a == answer);
		}


		public string GetAnswerDescription(Answer answer)
		{
			return TemplateQuestion.GetAnswerDescription(answer);
		}


		internal bool AllUsersHasAnswered(List<Guid> userIds)
		{
			return Answers.Count == userIds.Count;
		}


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


		internal void EnableAnswers()
		{
			IsUpForAnswer = true;
		}

		internal void DisableAnswers()
		{
			IsUpForAnswer = false;
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
