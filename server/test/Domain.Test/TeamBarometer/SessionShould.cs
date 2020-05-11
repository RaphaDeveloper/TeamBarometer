using Domain.TeamBarometer.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Domain.Test.TeamBarometer
{
	public class SessionShould
	{
		Guid facilitatorId = Guid.NewGuid();

		[Test]
		public void HasId()
		{
			Meeting session = CreateSession();

			Assert.That(session.Id, Is.Not.Null);
		}

		[Test]
		public void HasUniqueId()
		{
			Meeting firstSession = CreateSession();
			Meeting secondSession = CreateSession();
			Meeting thirdSession = CreateSession();

			Assert.That(firstSession.Id, Is.Not.EqualTo(secondSession.Id));
			Assert.That(secondSession.Id, Is.Not.EqualTo(thirdSession.Id));
		}

		[Test]
		public void HasReadOnlyId()
		{
			Meeting session = CreateSession();

			bool idIsReadOnly = !session.GetType().GetProperty(nameof(session.Id)).CanWrite;

			Assert.True(idIsReadOnly, "Session id is not read only");
		}

		private Meeting CreateSession()
		{
			List<TemplateQuestion> questionsTemplate = new List<TemplateQuestion>
			{
				new TemplateQuestion("Confiança", null),
				new TemplateQuestion("Feedback", null)
			};

			return new Meeting(facilitatorId, questionsTemplate);
		}
	}
}
