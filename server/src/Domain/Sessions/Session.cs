using Domain.Sessions.Events;
using DomainEventManager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Sessions
{
	public class Session
	{
		private Guid FacilitatorId { get; }
		private List<Guid> Participants { get; set; } = new List<Guid>();
		private bool SessionFinished => CurrentQuestionNode == null;
		private LinkedList<Question> LinkedQuestions { get; set; }
		private LinkedListNode<Question> CurrentQuestionNode { get; set; }
		
		
		public Session(Guid facilitatorId, IEnumerable<TemplateQuestion> questions)
		{
			FacilitatorId = facilitatorId;
			ConstructQuestionsOfThisSession(questions);
		}

		
		public Guid Id { get; } = Guid.NewGuid();
		public IEnumerable<Question> Questions => LinkedQuestions.Select(q => q);
		public Question CurrentQuestion => CurrentQuestionNode?.Value;
		public int NumberOfParticipants => Participants.Count;


		private void ConstructQuestionsOfThisSession(IEnumerable<TemplateQuestion> questions)
		{
			ConstructLinkedQuestions(questions);

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



		internal void AddParticipant(Guid userId)
		{
			if (!UserIsParticipating(userId))
				Participants.Add(userId);
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

			if (!SessionFinished)
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
