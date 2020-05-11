using Domain.Sessions.Entities;
using System.Collections.Generic;

namespace Domain.Sessions.Repositories
{
	public interface TemplateQuestionRepository
	{
		IEnumerable<TemplateQuestion> GetAll();
	}
}