using Domain.TeamBarometer.Events;
using DomainEventManager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.TeamBarometer.Entities
{
	public class Meeting
	{
		public Meeting(Guid facilitatorId, IEnumerable<TemplateQuestion> questions)
		{
			FacilitatorId = facilitatorId;
			ConstructQuestionsFromTemplate(questions);
		}


		private Guid FacilitatorId { get; }
		private List<Guid> Participants { get; set; } = new List<Guid>();
		private bool Finished => CurrentQuestionNode == null;
		private LinkedList<Question> LinkedQuestions { get; set; }
		private LinkedListNode<Question> CurrentQuestionNode { get; set; }

		public Guid Id { get; } = Guid.NewGuid();
		public IEnumerable<Question> Questions => LinkedQuestions.Select(q => q);
		public Question CurrentQuestion => CurrentQuestionNode?.Value;
		public int NumberOfParticipants => Participants.Count;


		internal void AddParticipant(Guid userId)
		{
			if (!UserIsParticipating(userId))
				Participants.Add(userId);
		}


		private void ConstructQuestionsFromTemplate(IEnumerable<TemplateQuestion> templateQuestions)
		{
			ConstructLinkedQuestions(templateQuestions);

			SetTheFirstQuestionsAsTheCurrent();
		}

		private void ConstructLinkedQuestions(IEnumerable<TemplateQuestion> templateQuestions)
		{
			IEnumerable<Question> questions = templateQuestions.Select(q => new Question(q));

			LinkedQuestions = new LinkedList<Question>(questions);
		}

		private void SetTheFirstQuestionsAsTheCurrent()
		{
			CurrentQuestionNode = LinkedQuestions.First;

			CurrentQuestionNode.Value.SetAsCurrent();
		}


		internal void AnswerTheCurrentQuestion(Guid userId, Answer answer, string annotation)
		{
			if (UserCanAnswerTheCurrentQuestion(userId))
			{
				CurrentQuestion.ContabilizeTheAnswer(userId, answer, annotation);

				if (CurrentQuestion.AllUsersHasAnswered(Participants))
				{
					CurrentQuestion.DisableAnswers();

					ChangeTheCurrentQuestion();

					DomainEvent.Dispatch(new WhenAllUsersAnswerTheQuestion(this));
				}
			}
		}

		private bool UserCanAnswerTheCurrentQuestion(Guid userId)
		{
			return UserIsParticipating(userId) && CurrentQuestion.IsUpForAnswer;
		}

		public bool UserIsParticipating(Guid userId)
		{
			return Participants.Contains(userId);
		}

		private void ChangeTheCurrentQuestion()
		{
			CurrentQuestionNode.Value.SetAsNotCurrent();

			SetTheNextQuestionAsTheCurrent();
		}

		private void SetTheNextQuestionAsTheCurrent()
		{
			CurrentQuestionNode = CurrentQuestionNode.Next;

			if (!Finished)
				CurrentQuestionNode.Value.SetAsCurrent();
		}


		internal void EnableAnswersOfTheCurrentQuestion(Guid userId)
		{
			if (UserIsTheFacilitator(userId))
			{
				CurrentQuestion.EnableAnswers();

				DomainEvent.Dispatch(new WhenTheQuestionIsEnabled(this));
			}
		}

		public bool UserIsTheFacilitator(Guid userId)
		{
			return FacilitatorId == userId;
		}
	}
}
