using NUnit.Framework;
using System;
using System.Linq;

namespace Domain.Test
{
	public class SessionServiceShould
	{
		[Test]
		public void CreateSession()
		{
			SessionService service = CreateService();

			Session session = service.CreateSession();

			Assert.That(session, Is.Not.Null);
		}

		[Test]
		public void CreateSessionWithQuestions()
		{
			SessionService service = CreateService();

			Session session = service.CreateSession();

			Assert.That(session.Questions, Is.Not.Null.And.Not.Empty);
		}

		[Test]
		public void PersistTheCreatedSession()
		{
			InMemorySessionRepository sessionRepository = new InMemorySessionRepository();
			SessionService service = CreateService(sessionRepository);

			Session session = service.CreateSession();

			Assert.That(session, Is.EqualTo(sessionRepository.GetById(session.Id)));
		}
		
		[Test]
		public void AnswerTheSessionQuestion()
		{
			SessionService service = CreateService();
			Session session = service.CreateSession();
			Question firstQuestion = session.Questions.ElementAt(0);
			Question secondQuestion = session.Questions.ElementAt(1);



			service.AnswerTheSessionQuestion(questionId: firstQuestion.Id, answer: Answer.Green, sessionId: session.Id);
			service.AnswerTheSessionQuestion(questionId: firstQuestion.Id, answer: Answer.Green, sessionId: session.Id);
			service.AnswerTheSessionQuestion(questionId: firstQuestion.Id, answer: Answer.Red, sessionId: session.Id);
			service.AnswerTheSessionQuestion(questionId: firstQuestion.Id, answer: Answer.Yellow, sessionId: session.Id);

			service.AnswerTheSessionQuestion(questionId: secondQuestion.Id, answer: Answer.Green, sessionId: session.Id);



			Answers firstQuestionAnswers = session.GetTheAnswersOfTheQuestion(firstQuestion.Id);
			Answers secondQuestionAnswers = session.GetTheAnswersOfTheQuestion(secondQuestion.Id);

			Assert.That(firstQuestionAnswers, Is.Not.Null);
			Assert.That(firstQuestionAnswers.GetAnswerCount(Answer.Green), Is.EqualTo(2));
			Assert.That(firstQuestionAnswers.GetAnswerCount(Answer.Red), Is.EqualTo(1));
			Assert.That(firstQuestionAnswers.GetAnswerCount(Answer.Yellow), Is.EqualTo(1));

			Assert.That(secondQuestionAnswers, Is.Not.Null);
			Assert.That(secondQuestionAnswers.GetAnswerCount(Answer.Green), Is.EqualTo(1));
			Assert.That(secondQuestionAnswers.GetAnswerCount(Answer.Red), Is.EqualTo(0));
			Assert.That(secondQuestionAnswers.GetAnswerCount(Answer.Yellow), Is.EqualTo(0));
		}

		private SessionService CreateService(InMemorySessionRepository sessionRepository = null)
		{
			sessionRepository ??= new InMemorySessionRepository();

			InMemoryQuestionRepository questionRepository = new InMemoryQuestionRepository();

			return new SessionService(sessionRepository, questionRepository);
		}
	}

	public class SessionShould
	{
		[Test]
		public void HasId()
		{
			Session session = CreateSession();

			Assert.That(session.Id, Is.Not.Null);
		}

		[Test]
		public void HasUniqueId()
		{
			Session firstSession = CreateSession();
			Session secondSession = CreateSession();
			Session thirdSession = CreateSession();

			Assert.That(firstSession.Id, Is.Not.EqualTo(secondSession.Id));
			Assert.That(secondSession.Id, Is.Not.EqualTo(thirdSession.Id));
		}

		[Test]
		public void HasReadOnlyId()
		{
			Session session = CreateSession();

			bool idIsReadOnly = !session.GetType().GetProperty(nameof(session.Id)).CanWrite;

			Assert.True(idIsReadOnly, "Session id is not read only");
		}

		private Session CreateSession()
		{
			return new Session(null);
		}
	}
}