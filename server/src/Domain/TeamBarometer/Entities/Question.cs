using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.TeamBarometer.Entities
{
	public class Question
	{
		public Question(TemplateQuestion templateQuestion)
		{
			TemplateQuestion = templateQuestion;
		}


		private TemplateQuestion TemplateQuestion { get; }
		private Dictionary<Guid, AnswerWithAnnotation> AnswerByUser { get; } = new Dictionary<Guid, AnswerWithAnnotation>();
		

        public Guid Id => TemplateQuestion.Id;
		public string Description => TemplateQuestion.Description;
		public bool IsUpForAnswer { get; private set; }
		public bool IsTheCurrent { get; private set; }


		public bool HasAnyAnswer()
		{
			return AnswerByUser.Any();
		}


		public int GetCountOfTheAnswer(Answer answer)
		{
			return AnswerByUser.Count(a => a.Value.Answer == answer);
		}


		public string GetAnswerDescription(Answer answer)
		{
			return TemplateQuestion.GetAnswerDescription(answer);
		}


		internal bool AllUsersHasAnswered(List<Guid> userIds)
		{
			return userIds.All(UserHasAlreadyAnswered);
		}


		internal void ContabilizeTheAnswer(Guid userId, Answer answer, string annoation)
		{
			if (!UserHasAlreadyAnswered(userId))
				AnswerByUser[userId] = new AnswerWithAnnotation(answer, annoation);
		}

		private bool UserHasAlreadyAnswered(Guid userId)
		{
			return AnswerByUser.ContainsKey(userId);
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
		

		public IEnumerable<AnswerWithAnnotation> GetAnswersWithAnnotation()
		{
			return AnswerByUser.Select(a => a.Value);
		}
	}
}
