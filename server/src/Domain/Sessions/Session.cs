using Domain.Sessions.Events;
using DomainEventManager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Sessions
{
	public class Session
	{
		public Session(Guid facilitatorId, IEnumerable<TemplateQuestion> questions)
		{
			FacilitatorId = facilitatorId;
			ConstructQuestionsOfThisSession(questions);
		}

		public Guid Id { get; } = Guid.NewGuid();
		public IEnumerable<Question> Questions => QuestionsById.Values;

		private Dictionary<Guid, Question> QuestionsById { get; set; } = new Dictionary<Guid, Question>();
		private List<Guid> Participants { get; set; } = new List<Guid>();
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

				if (this.currentQuestion != null)
				{
					this.currentQuestion.IsTheCurrent = true;
				}
			}
		}

		public int NumberOfParticipants => Participants.Count;

		private void ConstructQuestionsOfThisSession(IEnumerable<TemplateQuestion> questions)
		{
			IndexTheQuestionsById(questions);

			DefineTheCurrentQuestion();

			LinkTheQuestions();
		}

		private void IndexTheQuestionsById(IEnumerable<TemplateQuestion> questions)
		{
			foreach (TemplateQuestion question in questions)
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

		internal void AnswerTheCurrentQuestion(Guid userId, Answer answer)
		{
			if (UserIsParticipating(userId) && CurrentQuestion.IsUpForAnswer)
			{
				CurrentQuestion.ContabilizeTheAnswer(userId, answer);

				if (CurrentQuestion.AllUsersHasAnswered(Participants))
				{
					CurrentQuestion.DisableAnswers();

					ChangeTheCurrentQuestion();

					DomainEvent.Dispatch(new WhenAllUsersAnswerTheQuestion(this));
				}
			}
		}

		private void ChangeTheCurrentQuestion()
		{
			CurrentQuestion = CurrentQuestion.NextQuestion;
		}

		internal void AddParticipant(Guid userId)
		{
			if (!UserIsParticipating(userId))
			{
				Participants.Add(userId);
			}
		}

		internal void EnableAnswersOfTheCurrentQuestion(Guid userId)
		{
			if (UserIsTheFacilitator(userId))
			{
				CurrentQuestion.EnableAnswers();

				DomainEvent.Dispatch(new WhenTheQuestionIsEnabled(this));
			}
		}

		public bool UserIsParticipating(Guid userId)
		{
			return Participants.Contains(userId);
		}

		public bool UserIsTheFacilitator(Guid userId)
		{
			return FacilitatorId == userId;
		}
	}
}
