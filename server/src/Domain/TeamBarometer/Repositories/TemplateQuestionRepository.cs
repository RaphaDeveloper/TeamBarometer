using Domain.TeamBarometer.Entities;
using System.Collections.Generic;

namespace Domain.TeamBarometer.Repositories
{
	public interface TemplateQuestionRepository
	{
		IEnumerable<TemplateQuestion> GetAll();
	}
}