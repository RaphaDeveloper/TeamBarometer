using System.Collections.Generic;
using System.Linq;

namespace Domain.Questions
{
	public class InMemoryQuestionTemplateRepository
	{
		public IEnumerable<QuestionTemplate> GetAll()
		{
			return new List<QuestionTemplate>
			{
				new QuestionTemplate("Confiança"),
				new QuestionTemplate("Feedback")
			}.AsEnumerable();
		}
	}
}
