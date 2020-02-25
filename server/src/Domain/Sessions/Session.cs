﻿using Domain.Questions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Sessions
{
	public class Session
	{
		public Session(Guid facilitatorId, IEnumerable<QuestionTemplate> questions)
		{
			FacilitatorId = facilitatorId;
			ConstructQuestionsOfThisSession(questions);
		}

		public Guid Id { get; } = Guid.NewGuid();
		public IEnumerable<Question> Questions => QuestionsById.Values;

		private Dictionary<Guid, Question> QuestionsById { get; set; } = new Dictionary<Guid, Question>();
		private List<Guid> TeamMembers { get; set; } = new List<Guid>();
		private Guid FacilitatorId { get; }
		
		private Question currentQuestion;
		public Question CurrentQuestion 
		{
			get { return this.currentQuestion; }
			private set 
			{
				if (this.currentQuestion != null)
				{
					this.currentQuestion.IsTheCurrent = false;
				}

				this.currentQuestion = value;

				this.currentQuestion.IsTheCurrent = true;
			}
		}

		private void ConstructQuestionsOfThisSession(IEnumerable<QuestionTemplate> questions)
		{
			IndexTheQuestionsById(questions);

			DefineTheCurrentQuestion();

			LinkTheQuestions();
		}

		private void IndexTheQuestionsById(IEnumerable<QuestionTemplate> questions)
		{
			foreach (QuestionTemplate question in questions)
			{
				QuestionsById.Add(question.Id, new Question(question));
			}
		}

		private void DefineTheCurrentQuestion()
		{
			CurrentQuestion = Questions.First();
		}

		private void LinkTheQuestions()
		{
			Question priorQuestion = null;

			foreach (Question question in Questions)
			{
				if (priorQuestion != null)
				{
					priorQuestion.NextQuestion = question;
				}

				priorQuestion = question;
			}
		}

		internal void AnswerTheCurrentQuestion(Guid teamMemberId, Answer answer)
		{
			if (TeamMemberIsParticipating(teamMemberId) && CurrentQuestion.IsUpForAnswer)
			{
				CurrentQuestion.ContabilizeTheAnswer(teamMemberId, answer);

				if (CurrentQuestion.AllTeamMembersAnswered(TeamMembers))
				{
					CurrentQuestion.DisableAnswers();

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

		internal void EnableAnswersOfTheCurrentQuestion()
		{
			CurrentQuestion.EnableAnswers();
		}

		public bool TeamMemberIsParticipating(Guid teamMemberId)
		{
			return TeamMembers.Contains(teamMemberId);
		}

		public bool TeamMemberIsTheFacilitator(Guid teamMemberId)
		{
			return FacilitatorId == teamMemberId;
		}
	}
}
