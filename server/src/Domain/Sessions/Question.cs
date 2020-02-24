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
		private List<Guid> IdOfTeamMembersWhoAnswered { get; set; } = new List<Guid>();
		public Question NextQuestion { get; internal set; }
		public bool IsUpForAnswer { get; private set; }
		public bool IsTheCurrent { get; internal set; }

		internal void ContabilizeTheAnswer(Guid teamMemberId, Answer answer)
		{
			if (!TeamMemberHasAlreadyAnswered(teamMemberId))
			{
				Answers.Add(answer);

				IdOfTeamMembersWhoAnswered.Add(teamMemberId);
			}
		}

		private bool TeamMemberHasAlreadyAnswered(Guid teamMemberId)
		{
			return IdOfTeamMembersWhoAnswered.Contains(teamMemberId);
		}

		public bool HasAnyAnswer()
		{
			return Answers.Any();
		}

		public int GetCountOfTheAnswer(Answer answer)
		{
			return Answers.Count(a => a == answer);
		}

		internal bool AllTeamMembersAnswered(List<Guid> teamMembersId)
		{
			return Answers.Count == teamMembersId.Count;
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
