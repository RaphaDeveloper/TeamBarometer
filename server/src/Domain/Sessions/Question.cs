using Domain.Questions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Sessions
{
	public class Question
	{
		private readonly QuestionTemplate question;

		public Question(QuestionTemplate question)
		{
			this.question = question;
		}

		public Guid Id => question.Id;
		private List<Answer> Answers { get; set; } = new List<Answer>();
		private List<Guid> IdOfTheTeamMembersWhoAnswered { get; set; } = new List<Guid>();
		public Question NextQuestion { get; internal set; }
		public bool IsUpForAnswer { get; private set; }

		internal void ContabilizeTheAnswer(Guid teamMemberId, Answer answer)
		{
			if (!TeamMemberHasAlreadyAnswered(teamMemberId))
			{
				Answers.Add(answer);

				IdOfTheTeamMembersWhoAnswered.Add(teamMemberId);
			}
		}

		private bool TeamMemberHasAlreadyAnswered(Guid teamMemberId)
		{
			return IdOfTheTeamMembersWhoAnswered.Contains(teamMemberId);
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
	}
}
