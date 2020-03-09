using Domain.Sessions;
using NUnit.Framework;
using System.Collections.Generic;

namespace Domain.Test.Sessions
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

			Assert.That(question.GetDescriptionOfTheAnswer(Answer.Green), Is.Not.Empty.And.Not.Null.And.EqualTo(questionTemplate.GetDescriptionOfTheAnswer(Answer.Green)));
			Assert.That(question.GetDescriptionOfTheAnswer(Answer.Red), Is.Not.Empty.And.Not.Null.And.EqualTo(questionTemplate.GetDescriptionOfTheAnswer(Answer.Red)));
			Assert.That(question.GetDescriptionOfTheAnswer(Answer.Yellow), Is.Null);
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
