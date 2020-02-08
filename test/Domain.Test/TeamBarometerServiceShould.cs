using NUnit.Framework;

namespace Domain.Test
{
	public class TeamBarometerServiceShould
	{
		[SetUp]
		public void Setup()
		{
		}

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

		private static TeamBarometerService CreateService()
		{
			InMemoryQuestionRepository questionRepository = new InMemoryQuestionRepository();

			return new TeamBarometerService(questionRepository);
		}
	}

	public class SessionShould
	{
		[Test]
		public void HasId()
		{
			Session session = CreateSession();

			Assert.That(session.Id, Is.Not.Null.And.Not.Empty);
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