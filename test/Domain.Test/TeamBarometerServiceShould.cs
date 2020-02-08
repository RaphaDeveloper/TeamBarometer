using NUnit.Framework;
using System;
using System.Linq;

namespace Domain.Test
{
	public class TeamBarometerServiceShould
	{
		[Test]
		public void GenerateSession()
		{
			TeamBarometerService service = CreateService();

			Session session = service.CreateSession();

			Assert.That(session, Is.Not.Null);
		}

		[Test]
		public void GenerateSessionWithQuestions()
		{
			TeamBarometerService service = CreateService();

			Session session = service.CreateSession();

			Assert.That(session.Questions, Is.Not.Null.And.Not.Empty);
		}

		[Test]
		public void PersistTheGeneratedSession()
		{
			InMemorySessionRepository sessionRepository = new InMemorySessionRepository();
			TeamBarometerService service = CreateService(sessionRepository);

			Session session = service.CreateSession();

			Assert.That(session, Is.EqualTo(sessionRepository.GetById(session.Id)));
		}
		
		[Test]
		public void CheckTheQuestionChoiceOnSession()
		{
			TeamBarometerService service = CreateService();
			Session session = service.CreateSession();
			Question question = session.Questions.First();

			service.CheckTheQuestionChoiceOnSession(questionId: question.Id, questionChoice: QuestionChoice.Green, sessionId: session.Id);


			ChoicesOfQuestion questionOfChoices = session.GetChoicesOfQuestion(question.Id);
			
			Assert.That(questionOfChoices, Is.Not.Null);
			Assert.That(questionOfChoices.GetCountByChoice(QuestionChoice.Green), Is.EqualTo(1));
		}

		private TeamBarometerService CreateService(InMemorySessionRepository sessionRepository = null)
		{
			sessionRepository = sessionRepository ?? new InMemorySessionRepository();

			InMemoryQuestionRepository questionRepository = new InMemoryQuestionRepository();

			return new TeamBarometerService(sessionRepository, questionRepository);
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