using System;

namespace Domain.Questions
{
	public class QuestionTemplate
	{
		public QuestionTemplate(string description)
		{
			Description = description;
		}

		public Guid Id { get; } = Guid.NewGuid();
		public string Description { get; set; }
	}
}
