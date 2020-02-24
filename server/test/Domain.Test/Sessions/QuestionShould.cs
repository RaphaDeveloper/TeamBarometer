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
			QuestionTemplate questionTemplate = new QuestionTemplate();

			Question question = new Question(questionTemplate);

			Assert.That(question.Id, Is.EqualTo(questionTemplate.Id));
		}
	}
}
