using Domain.Sessions;
using Domain.Sessions.Repositories;
using Domain.Sessions.UseCases;
using NUnit.Framework;
using System;
using System.Linq;

namespace Domain.Test.Sessions.UseCases
{
	public class SessionServiceShould
	{
		private Guid facilitatorId = Guid.NewGuid();


		#region Create Session

		[Test]
		public void CreateSessionWithFacilitatorAndQuestions()
		{
			SessionService service = CreateService();

			Session session = service.CreateSession(facilitatorId);

			Assert.That(session, Is.Not.Null);
			Assert.IsTrue(session.UserIsTheFacilitator(facilitatorId));
			Assert.That(session.Questions, Is.Not.Null.And.Not.Empty);
		}

		[Test]
		public void PersistTheCreatedSession()
		{
			InMemorySessionRepository sessionRepository = new InMemorySessionRepository();
			SessionService service = CreateService(sessionRepository);

			Session session = service.CreateSession(facilitatorId);

			Assert.That(session, Is.EqualTo(sessionRepository.GetById(session.Id)));
		}

		[Test]
		public void CreateTheSessionWithTheFirstQuestionBeingTheCurrent()
		{
			SessionService service = CreateService();

			Session session = service.CreateSession(facilitatorId);

			Assert.That(session.Questions.First(), Is.EqualTo(session.CurrentQuestion));
			Assert.True(session.CurrentQuestion.IsTheCurrent);
		}

		#endregion


		#region Answer The Current Question

		[Test]
		public void AnswerTheSessionQuestion()
		{
			Guid userId = Guid.NewGuid();
			SessionService service = CreateService();
			Session session = service.CreateSession(facilitatorId);
			Question currentQuestion = session.CurrentQuestion;
			service.JoinTheSession(session.Id, userId);
			service.EnableAnswersOfTheCurrentQuestion(session.Id);


			service.AnswerTheSessionCurrentQuestion(userId, Answer.Red, session.Id);


			Assert.That(currentQuestion.GetCountOfTheAnswer(Answer.Red), Is.EqualTo(1));
		}

		[Test]
		public void ChangeTheCurrentQuestionWhenAllUsersAnswerTheQuestion()
		{
			Guid firstUserId = Guid.NewGuid();
			Guid secondUserId = Guid.NewGuid();
			SessionService service = CreateService();
			Session session = service.CreateSession(facilitatorId);
			Question priorQuestion = session.CurrentQuestion;
			service.JoinTheSession(session.Id, firstUserId);
			service.JoinTheSession(session.Id, secondUserId);
			service.EnableAnswersOfTheCurrentQuestion(session.Id);


			service.AnswerTheSessionCurrentQuestion(firstUserId, Answer.Green, session.Id);
			service.AnswerTheSessionCurrentQuestion(secondUserId, Answer.Green, session.Id);


			Assert.That(priorQuestion.NextQuestion, Is.EqualTo(session.CurrentQuestion));
			Assert.True(session.CurrentQuestion.IsTheCurrent);
			Assert.False(priorQuestion.IsTheCurrent);
		}

		[Test]
		public void DisableAnswersOfTheQuestionWhenAllUsersAnswerIt()
		{
			Guid firstUserId = Guid.NewGuid();
			Guid secondUserId = Guid.NewGuid();
			SessionService service = CreateService();
			Session session = service.CreateSession(facilitatorId);
			Question currentQuestion = session.CurrentQuestion;
			service.JoinTheSession(session.Id, firstUserId);
			service.JoinTheSession(session.Id, secondUserId);
			service.EnableAnswersOfTheCurrentQuestion(session.Id);


			service.AnswerTheSessionCurrentQuestion(firstUserId, Answer.Green, session.Id);
			service.AnswerTheSessionCurrentQuestion(secondUserId, Answer.Green, session.Id);


			Assert.False(currentQuestion.IsUpForAnswer);
		}

		[Test]
		public void NotAnswerTheSessionQuestionMoreThanOnceForTheSameUser()
		{
			Guid userId = Guid.NewGuid();
			SessionService service = CreateService();
			Session session = service.CreateSession(facilitatorId);
			Question currentQuestion = session.CurrentQuestion;
			service.JoinTheSession(session.Id, userId);
			service.EnableAnswersOfTheCurrentQuestion(session.Id);


			service.AnswerTheSessionCurrentQuestion(userId, Answer.Green, session.Id);
			service.AnswerTheSessionCurrentQuestion(userId, Answer.Green, session.Id);


			Assert.That(currentQuestion.GetCountOfTheAnswer(Answer.Green), Is.EqualTo(1));
		}

		[Test]
		public void NotAnswerTheSessionQuestionWhenTheUserIsNotInTheSession()
		{
			Guid userId = Guid.NewGuid();
			SessionService service = CreateService();
			Session session = service.CreateSession(facilitatorId);
			Question currentQuestion = session.CurrentQuestion;
			service.EnableAnswersOfTheCurrentQuestion(session.Id);


			service.AnswerTheSessionCurrentQuestion(userId, Answer.Red, session.Id);


			Assert.False(currentQuestion.HasAnyAnswer());
		}

		[Test]
		public void NotAnswerTheSessionQuestionWhenTheQuestionsIsNotUpForAnswer()
		{
			Guid firstUserId = Guid.NewGuid();
			SessionService service = CreateService();
			Session session = service.CreateSession(facilitatorId);
			Question currentQuestion = session.CurrentQuestion;
			service.JoinTheSession(session.Id, firstUserId);


			service.AnswerTheSessionCurrentQuestion(firstUserId, Answer.Green, session.Id);


			Assert.False(currentQuestion.HasAnyAnswer());
		}

		#endregion


		#region JoinTheSession

		[Test]
		public void JoinTheSession()
		{
			SessionService sessionService = CreateService();
			Session session = sessionService.CreateSession(facilitatorId);
			Guid userId = Guid.NewGuid();

			sessionService.JoinTheSession(session.Id, userId);

			Assert.True(session.UserIsParticipating(userId));
		}

		[Test]
		public void NotJoinTheSessionIfTheUserIsAlreadyInTheSession()
		{
			SessionService sessionService = CreateService();
			Session session = sessionService.CreateSession(facilitatorId);
			Guid userId = Guid.NewGuid();

			sessionService.JoinTheSession(session.Id, userId);
			sessionService.JoinTheSession(session.Id, userId);

			Assert.That(session.NumberOfParticipants, Is.EqualTo(1));
		}

		#endregion


		#region EnableAnswers

		[Test]
		public void EnableAnswersOfTheCurrentQuestionOfTheSession()
		{
			SessionService service = CreateService();
			Session session = service.CreateSession(facilitatorId);


			service.EnableAnswersOfTheCurrentQuestion(session.Id);


			Assert.True(session.CurrentQuestion.IsUpForAnswer);
		}

		#endregion


		private SessionService CreateService(InMemorySessionRepository sessionRepository = null)
		{
			sessionRepository ??= new InMemorySessionRepository();

			InMemoryTemplateQuestionRepository questionRepository = new InMemoryTemplateQuestionRepository();

			return new SessionService(sessionRepository, questionRepository);
		}
	}
}
