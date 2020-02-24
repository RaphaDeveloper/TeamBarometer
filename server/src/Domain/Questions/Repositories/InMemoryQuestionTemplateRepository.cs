using Domain.Sessions;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Questions
{
	public class InMemoryQuestionTemplateRepository
	{
		public IEnumerable<QuestionTemplate> GetAll()
		{
			Dictionary<Answer, string> descriptionByAnswerConfianca = new Dictionary<Answer, string>
			{
				{ Answer.Red, "Não somos confiantes" },
				{ Answer.Green, "Somos confiantes" }
			};

			Dictionary<Answer, string> descriptionByAnswerFeedback = new Dictionary<Answer, string>
			{
				{ Answer.Red, "Não damos feedback" },
				{ Answer.Green, "Damos feedback" }
			};

			return new List<QuestionTemplate>
			{
				new QuestionTemplate("Confiança", descriptionByAnswerConfianca),
				new QuestionTemplate("Feedback", descriptionByAnswerFeedback)
			}.AsEnumerable();
		}
	}
}
