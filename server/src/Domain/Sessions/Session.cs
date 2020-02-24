using Domain.Questions;
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
		public Question CurrentQuestion { get; private set; }
		private List<Guid> TeamMembers { get; set; } = new List<Guid>();
		public Guid FacilitatorId { get; }

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
			CurrentQuestion = QuestionsById.Values.First();
		}

		private void LinkTheQuestions()
		{
			Question priorQuestion = null;

			foreach (Question question in QuestionsById.Values)
			{
				if (priorQuestion != null)
				{
					priorQuestion.NextQuestion = question;
				}

				priorQuestion = question;
			}
		}
	}
}
