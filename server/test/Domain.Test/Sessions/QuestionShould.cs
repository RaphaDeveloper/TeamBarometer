using Domain.Questions;
using Domain.Sessions;
using NUnit.Framework;

namespace Domain.Test.Sessions
{
	public class QuestionShould
	{
		[Test]
		public void BeCreatedFromQuestionTemplate()
		{
			QuestionTemplate questionTemplate = new QuestionTemplate("Feedback");

			Question question = new Question(questionTemplate);

			Assert.NotNull(question);
		}

		[Test]
		public void HasTheSameIdOfTheTemplate()
		{
			QuestionTemplate questionTemplate = new QuestionTemplate("Feedback");

			Question question = new Question(questionTemplate);

			Assert.That(question.Id, Is.EqualTo(questionTemplate.Id));
		}

		[Test]
		public void HasTheSameDescriptionOfTheTemplate()
		{
			QuestionTemplate questionTemplate = new QuestionTemplate("Feedback");

			Question question = new Question(questionTemplate);

			Assert.That(question.Description, Is.EqualTo(questionTemplate.Description));
		}
	}
}
