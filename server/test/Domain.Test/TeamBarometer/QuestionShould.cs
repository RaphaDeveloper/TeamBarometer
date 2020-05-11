using Domain.TeamBarometer.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace Domain.Test.TeamBarometer
{
	public class QuestionShould
	{
		[Test]
		public void BeCreatedFromQuestionTemplate()
		{
			TemplateQuestion questionTemplate = CreateQuestionTemplate();

			Question question = new Question(questionTemplate);

			Assert.NotNull(question);
		}

		[Test]
		public void HasTheSameIdOfTheTemplate()
		{
			TemplateQuestion questionTemplate = CreateQuestionTemplate();

			Question question = new Question(questionTemplate);

			Assert.That(question.Id, Is.EqualTo(questionTemplate.Id));
		}

		[Test]
		public void HasTheSameDescriptionOfTheTemplate()
		{
			TemplateQuestion questionTemplate = CreateQuestionTemplate();

			Question question = new Question(questionTemplate);

			Assert.That(question.Description, Is.EqualTo(questionTemplate.Description));
		}

		[Test]
		public void HasTheSameAnswerDescriptionOfTheTemplate()
		{
			TemplateQuestion questionTemplate = CreateQuestionTemplate();

			Question question = new Question(questionTemplate);

			Assert.That(question.GetAnswerDescription(Answer.Green), Is.Not.Empty.And.Not.Null.And.EqualTo(questionTemplate.GetAnswerDescription(Answer.Green)));
			Assert.That(question.GetAnswerDescription(Answer.Red), Is.Not.Empty.And.Not.Null.And.EqualTo(questionTemplate.GetAnswerDescription(Answer.Red)));
			Assert.That(question.GetAnswerDescription(Answer.Yellow), Is.Null);
		}

		private TemplateQuestion CreateQuestionTemplate()
		{
			Dictionary<Answer, string> descriptionByAnswer = new Dictionary<Answer, string>
			{
				{ Answer.Red, "Não damos feedback" },
				{ Answer.Green, "Damos feedback" }
			};

			return new TemplateQuestion("Feedback", descriptionByAnswer);
		}
	}
}
