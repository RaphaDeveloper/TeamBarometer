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
				CreateQuestion(),
				CreateQuestion()
			}.AsEnumerable();
		}

		private QuestionTemplate CreateQuestion()
		{
			return new QuestionTemplate();
		}
	}
}
