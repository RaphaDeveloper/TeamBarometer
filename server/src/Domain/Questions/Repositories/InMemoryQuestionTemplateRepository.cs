using Domain.Sessions;
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
				new QuestionTemplate("Confiança", null),
				new QuestionTemplate("Feedback", null)
			}.AsEnumerable();
		}
	}
}
