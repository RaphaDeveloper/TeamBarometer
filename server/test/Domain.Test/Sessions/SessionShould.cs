using Domain.Questions;
using Domain.Sessions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Domain.Test.Sessions
{
	public class SessionShould
	{
		Guid facilitatorId = Guid.NewGuid();

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
			List<QuestionTemplate> questionsTemplate = new List<QuestionTemplate>
			{
				new QuestionTemplate("Confiança", null),
				new QuestionTemplate("Feedback", null)
			};

			return new Session(facilitatorId, questionsTemplate);
		}
	}
}
