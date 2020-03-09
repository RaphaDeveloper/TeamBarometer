﻿using System;
using System.Collections.Generic;

namespace Domain.Sessions
{
	public class TemplateQuestion
	{
		public TemplateQuestion(string description, Dictionary<Answer, string> descriptionByAnswer)
		{
			Description = description;
			DescriptionByAnswer = descriptionByAnswer;
		}

		public Guid Id { get; } = Guid.NewGuid();
		public string Description { get; set; }

		public string GetDescriptionOfTheAnswer(Answer answer)
		{
			DescriptionByAnswer.TryGetValue(answer, out string description);

			return description;
		}

		private Dictionary<Answer, string> DescriptionByAnswer { get; }
	}
}