using Domain.TeamBarometer.Entities;
using Domain.TeamBarometer.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Test.Sessions.Doubles.Repositories
{
	public class FakeTemplateQuestionRepository : TemplateQuestionRepository
	{
		public IEnumerable<TemplateQuestion> GetAll()
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

			return new List<TemplateQuestion>
			{
				new TemplateQuestion("Confiança", descriptionByAnswerConfianca),
				new TemplateQuestion("Feedback", descriptionByAnswerFeedback)
			}.AsEnumerable();
		}
	}
}
